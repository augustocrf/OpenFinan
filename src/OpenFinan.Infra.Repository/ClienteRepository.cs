using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenFinan.Domain.Entity;
using OpenFinan.Domain.Repositories;
using Dapper;


namespace OpenFinan.Infra.Repository;

public class ClienteRepository : IClienteReadOnlyRepository, IClienteWriteOnlyRepository
{
    private readonly IDbConnection dbConnection;

    private ClienteRepository() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

    public ClienteRepository (IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
    }

    public async Task AtualizaClienteAsync(ClienteEntity cliente)
    {
        var clienteParams = new DynamicParameters();
        clienteParams.Add("cpf", cliente.cpf);
        clienteParams.Add("nome", cliente.nome);
        clienteParams.Add("uf", cliente.uf);
        clienteParams.Add("celular", cliente.celular);

        var queryCliente = @"UPDATE Cliente SET nome = @nome, uf = @uf, @celular = @celular  WHERE cpf = @cpf";

        await dbConnection.ExecuteAsync(queryCliente, clienteParams);
    }

    public async Task<bool> ClienteExisteAsync(int cpf)
    {
        var clienteParams = new DynamicParameters();
        clienteParams.Add("cpf", cpf);

        var query = @"select count(Id) from Cliente Where cpf = @cpf";

        var result = await dbConnection.ExecuteScalarAsync(query, clienteParams);

        return Convert.ToUInt16(result) > 0;
    }

    public async Task ExcluiClienteAsync(int cpf)
    {
        var clienteParams = new DynamicParameters();
        clienteParams.Add("cpf", cpf);

        var query = @"delete from Cliente Where cpf = @cpf";

        await dbConnection.ExecuteScalarAsync<int>(query, clienteParams);
    }

    public async Task IncluiClienteAsync(ClienteEntity cliente)
    {
        if (cliente == null)
            throw new ArgumentNullException(nameof(cliente));

        var clienteParams = new DynamicParameters();
        clienteParams.Add("cpf", cliente.cpf);
        clienteParams.Add("nome", cliente.nome);
        clienteParams.Add("uf", cliente.uf);
        clienteParams.Add("celular", cliente.celular);

        var queryCliente = @"INSERT INTO Cliente (cpf, nome, uf, celular) 
                            VALUES (@cpf, @nome, @uf, @celular)";

        await dbConnection.ExecuteAsync(queryCliente, clienteParams);
    }

    public async Task<IEnumerable<ClienteEntity>> ListaClientesAsync()
    {
        var query = @"select cpf, Nome, uf, celular from Cliente";    

        var clientes = await dbConnection.QueryAsync<ClienteEntity>(query);

        return clientes;
    }

    public async Task<ClienteEntity> RetornaClienteAsync(int cpf)
    {
        var clienteParams = new DynamicParameters();
        clienteParams.Add("cpf", cpf);

        var query = @"select cpf, nome, uf, celular from Cliente Where cpf = @cpf";

        var cliente = await dbConnection.QueryAsync<ClienteEntity>(query, clienteParams);

        return cliente.SingleOrDefault();
    }

}
