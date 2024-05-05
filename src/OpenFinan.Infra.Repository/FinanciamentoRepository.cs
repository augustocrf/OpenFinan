using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenFinan.Domain.Entity;
using OpenFinan.Domain.Repositories;
using Dapper;


namespace OpenFinan.Infra.Repository;

public class FinanciamentoRepository : IFinanciamentoReadOnlyRepository, IFinanciamentoWriteOnlyRepository
{
    private readonly IDbConnection dbConnection;

    private FinanciamentoRepository() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

    public FinanciamentoRepository (IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
    }

    public async Task AtualizaFinanciamentoAsync(FinanciamentoEntity financiamento)
    {
        var financiamentoParams = new DynamicParameters();
        financiamentoParams.Add("idfinanciamento", financiamento.idfinanciamento);
        financiamentoParams.Add("idtipofinanciamento", financiamento.idtipofinanciamento);
        financiamentoParams.Add("valortotal", financiamento.valortotal);
        financiamentoParams.Add("dataultimovencimento", financiamento.dataultimovencimento);

        var queryFinanciamento = @"UPDATE Financiamento SET 
                                        idtipofinaniamento = @idtipofinaniamento,
                                        valortotal = @valortotal,
                                        dataultimovencimento = @dataultimovencimento
                                        WHERE idfinanciamento = @idfinanciamento";

        await dbConnection.ExecuteAsync(queryFinanciamento, financiamentoParams);
    }

    public async Task<bool> FinanciamentoExisteAsync(int idfinanciamento)
    {
        var financiamentoParams = new DynamicParameters();
        financiamentoParams.Add("idfinanciamento", idfinanciamento);

        var query = @"select count(idfinanciamento) from Financiamento 
                        Where idfinanciamento = @idfinanciamento";

        var result = await dbConnection.ExecuteScalarAsync(query, financiamentoParams);

        return Convert.ToUInt16(result) > 0;
    }

    public async Task<bool> FinanciamentoClienteExisteAsync(int cpf)
    {
        var financiamentoParams = new DynamicParameters();
        financiamentoParams.Add("cpf", cpf);

        var query = @"select count(idfinanciamento) from Financiamento 
                        Where cpf = @cpf";

        var result = await dbConnection.ExecuteScalarAsync(query, financiamentoParams);

        return Convert.ToUInt16(result) > 0;
    }

    public async Task ExcluiFinanciamentoAsync(int idfinanciamento)
    {
        var financiamentoParams = new DynamicParameters();
        financiamentoParams.Add("idfinanciamento", idfinanciamento);

        var query = @"delete from Financiamento Where idfinanciamento = @idfinanciamento";

        await dbConnection.ExecuteScalarAsync<int>(query, financiamentoParams);
    }

    public async Task ExcluiFinanciamentoClienteAsync(int cpf)
    {
        var financiamentoParams = new DynamicParameters();
        financiamentoParams.Add("cpf", cpf);

        var query = @"delete from Financiamento Where cpf = @cpf";

        await dbConnection.ExecuteScalarAsync<int>(query, financiamentoParams);
    }

    public async Task IncluiFinanciamentoAsync(FinanciamentoEntity financiamento)
    {
        if (financiamento == null)
            throw new ArgumentNullException(nameof(financiamento));

        var financiamentoParams = new DynamicParameters();
        financiamentoParams.Add("cpf", financiamento.cpf);
        financiamentoParams.Add("idtipofinanciamento", financiamento.idtipofinanciamento);
        financiamentoParams.Add("valortotal", financiamento.valortotal);
        financiamentoParams.Add("dataultimovencimento", financiamento.dataultimovencimento);

        var queryFinanciamento = @"INSERT INTO Financiamento (cpf, idtipofinanciamento, valortotal, dataultimovencimento) 
                            VALUES (@cpf, @idtipofinanciamento, @valortotal, @dataultimovencimento)";

        await dbConnection.ExecuteAsync(queryFinanciamento, financiamentoParams);
    }

    public async Task<IEnumerable<FinanciamentoEntity>> ListaFinanciamentosAsync()
    {
        var query = @"select 
                        idfinanciamento, cpf, idtipofinanciamento, 
                        valortotal, dataultimovencimento 
                        from Financiamento";    

        var financiamentos = await dbConnection.QueryAsync<FinanciamentoEntity>(query);

        return financiamentos;
    }

    public async Task<FinanciamentoEntity> RetornaFinanciamentoAsync(int idfinanciamento)
    {
        var financiamentoParams = new DynamicParameters();
        financiamentoParams.Add("idfinanciamento", idfinanciamento);

        var query = @"select 
                        idfinanciamento, cpf, idtipofinanciamento, 
                        valortotal, dataultimovencimento 
                        from Financiamento 
                        Where idfinanciamento = @idfinanciamento";

        var financiamento = await dbConnection.QueryAsync<FinanciamentoEntity>(query, financiamentoParams);

        return financiamento.SingleOrDefault();
    }
}