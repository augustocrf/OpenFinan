using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenFinan.Domain.Entity;
using OpenFinan.Domain.Repositories;
using Dapper;


namespace OpenFinan.Infra.Repository;

public class TipoFinanciamentoRepository : ITipoFinanciamentoReadOnlyRepository, ITipoFinanciamentoWriteOnlyRepository
{
    private readonly IDbConnection dbConnection;

    private TipoFinanciamentoRepository() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

    public TipoFinanciamentoRepository (IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
    }

    public async Task AtualizaTipoFinanciamentoAsync(TipoFinanciamentoEntity tipofinanciamento)
    {
        var TipoFinanciamentoParams = new DynamicParameters();
        TipoFinanciamentoParams.Add("idtipofinanciamento", tipofinanciamento.idtipofinanciamento);
        TipoFinanciamentoParams.Add("descricao", tipofinanciamento.descricao);
        TipoFinanciamentoParams.Add("taxa", tipofinanciamento.taxa);

        var queryTipoFinanciamento = @"UPDATE TipoFinanciamento SET descricao = @descricao, taxa = @taxa  WHERE idtipofinanciamento = @idtipofinanciamento";

        await dbConnection.ExecuteAsync(queryTipoFinanciamento, TipoFinanciamentoParams);
    }

    public async Task<bool> TipoFinanciamentoExisteAsync(int idtipofinanciamento)
    {
        var TipoFinanciamentoParams = new DynamicParameters();
        TipoFinanciamentoParams.Add("idtipofinanciamento", idtipofinanciamento);

        var query = @"select count(idtipofinanciamento) from TipoFinanciamento Where idtipofinanciamento = @idtipofinanciamento";

        var result = await dbConnection.ExecuteScalarAsync(query, TipoFinanciamentoParams);

        return Convert.ToUInt16(result) > 0;
    }

    public async Task ExcluiTipoFinanciamentoAsync(int idtipofinanciamento)
    {
        var tipoFinanciamentoParams = new DynamicParameters();
        tipoFinanciamentoParams.Add("idtipofinanciamento", idtipofinanciamento);

        var query = @"delete from TipoFinanciamento Where idtipofinanciamento = @idtipofinanciamento";

        await dbConnection.ExecuteScalarAsync<int>(query, tipoFinanciamentoParams);
    }

    public async Task IncluiTipoFinanciamentoAsync(TipoFinanciamentoEntity tipofinanciamento)
    {
        if (tipofinanciamento == null)
            throw new ArgumentNullException(nameof(tipofinanciamento));

        var tipofinanciamentoParams = new DynamicParameters();
        tipofinanciamentoParams.Add("descricao", tipofinanciamento.descricao);
        tipofinanciamentoParams.Add("taxa", tipofinanciamento.taxa);

        var queryTipoFinanciamento = @"INSERT INTO TipoFinanciamento (descricao, taxa) 
                            VALUES (@descricao, @taxa)";

        await dbConnection.ExecuteAsync(queryTipoFinanciamento, tipofinanciamentoParams);
    }

    public async Task<IEnumerable<TipoFinanciamentoEntity>> ListaTipoFinanciamentosAsync()
    {
        var query = @"select descricao, taxa from TipoFinanciamento";    

        var tipofinanciamentos = await dbConnection.QueryAsync<TipoFinanciamentoEntity>(query);

        return tipofinanciamentos;
    }

    public async Task<TipoFinanciamentoEntity> RetornaTipoFinanciamentoAsync(int idtipofinanciamento)
    {
        var tipoFinanciamentoParams = new DynamicParameters();
        tipoFinanciamentoParams.Add("idtipofinanciamento", idtipofinanciamento);

        var query = @"select idtipofinanciamento, descricao, taxa from TipoFinanciamento Where idtipofinanciamento = @idtipofinanciamento";

        var tipofinanciamento = await dbConnection.QueryAsync<TipoFinanciamentoEntity>(query, tipoFinanciamentoParams);

        return tipofinanciamento.SingleOrDefault();
    }

}
