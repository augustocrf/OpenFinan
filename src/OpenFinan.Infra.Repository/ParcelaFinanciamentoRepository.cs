using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenFinan.Domain.Entity;
using OpenFinan.Domain.Repositories;
using Dapper;


namespace OpenFinan.Infra.Repository;

public class ParcelaFinanciamentoRepository : IParcelaFinanciamentoReadOnlyRepository, IParcelaFinanciamentoWriteOnlyRepository
{
    private readonly IDbConnection dbConnection;

    private ParcelaFinanciamentoRepository() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

    public ParcelaFinanciamentoRepository (IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
    }

    public async Task AtualizaParcelaFinanciamentoAsync(ParcelaFinanciamentoEntity parcelafinanciamento)
    {
        var parcelafinanciamentoParams = new DynamicParameters();
        parcelafinanciamentoParams.Add("idparcelafinanciamento", parcelafinanciamento.idparcelafinanciamento);
        parcelafinanciamentoParams.Add("numeroparcela", parcelafinanciamento.numeroparcela);
        parcelafinanciamentoParams.Add("valorparcela", parcelafinanciamento.valorparcela);
        parcelafinanciamentoParams.Add("datavencimento", parcelafinanciamento.datavencimento);
        parcelafinanciamentoParams.Add("datapagamento", parcelafinanciamento.datapagamento);

        var queryParcelaFinanciamento = @"UPDATE ParcelaFinanciamento SET 
                                        numeroparcela = @numeroparcela,
                                        valorparcela = @valorparcela,
                                        datavencimento = @datavencimento,
                                        datapagamento = @datapagamento
                                        WHERE idparcelafinanciamento = @idparcelafinanciamento";

        await dbConnection.ExecuteAsync(queryParcelaFinanciamento, parcelafinanciamentoParams);
    }

    public async Task<bool> ParcelaFinanciamentoExisteAsync(int idparcelafinanciamento)
    {
        var parcelafinanciamentoParams = new DynamicParameters();
        parcelafinanciamentoParams.Add("idparcelafinanciamento", idparcelafinanciamento);

        var query = @"select count(idparcelafinanciamento) from ParcelaFinanciamento 
                        Where idparcelafinanciamento = @idparcelafinanciamento";

        var result = await dbConnection.ExecuteScalarAsync(query, parcelafinanciamentoParams);

        return Convert.ToUInt16(result) > 0;
    }

    public async Task<bool> ParcelasFinanciamentoExisteAsync(int idfinanciamento)
    {
        var parcelasfinanciamentoParams = new DynamicParameters();
        parcelasfinanciamentoParams.Add("idfinanciamento", idfinanciamento);

        var query = @"select count(idparcelafinanciamento) from ParcelaFinanciamento 
                        Where idfinanciamento = @idfinanciamento";

        var result = await dbConnection.ExecuteScalarAsync(query, parcelasfinanciamentoParams);

        return Convert.ToUInt16(result) > 0;
    }

    public async Task ExcluiParcelaFinanciamentoAsync(int idparcelafinanciamento)
    {
        var parcelafinanciamentoParams = new DynamicParameters();
        parcelafinanciamentoParams.Add("idparcelafinanciamento", idparcelafinanciamento);

        var query = @"delete from ParcelaFinanciamento Where idparcelafinanciamento = @idparcelafinanciamento";

        await dbConnection.ExecuteScalarAsync<int>(query, parcelafinanciamentoParams);
    }

    public async Task ExcluiParcelasFinanciamentoAsync(int idfinanciamento)
    {
        var parcelasfinanciamentoParams = new DynamicParameters();
        parcelasfinanciamentoParams.Add("idfinanciamento", idfinanciamento);

        var query = @"delete from ParcelaFinanciamento Where idfinanciamento = @idfinanciamento";

        await dbConnection.ExecuteScalarAsync<int>(query, parcelasfinanciamentoParams);
    }

    public async Task IncluiParcelaFinanciamentoAsync(ParcelaFinanciamentoEntity parcelafinanciamento)
    {
        if (parcelafinanciamento == null)
            throw new ArgumentNullException(nameof(parcelafinanciamento));

        var parcelafinanciamentoParams = new DynamicParameters();
        parcelafinanciamentoParams.Add("idfinanciamento", parcelafinanciamento.idfinanciamento);
        parcelafinanciamentoParams.Add("numeroparcela", parcelafinanciamento.numeroparcela);
        parcelafinanciamentoParams.Add("valorparcela", parcelafinanciamento.valorparcela);
        parcelafinanciamentoParams.Add("datavencimento", parcelafinanciamento.datavencimento);

        var queryParcelaFinanciamento = @"INSERT INTO ParcelaFinanciamento (idfinanciamento, numeroparcela, valorparcela, datavencimento) 
                            VALUES (@idfinanciamento, @numeroparcela, @valorparcela, @datavencimento)";

        await dbConnection.ExecuteAsync(queryParcelaFinanciamento, parcelafinanciamentoParams);
    }

    public async Task<IEnumerable<ParcelaFinanciamentoEntity>> ListaParcelasFinanciamentosAsync()
    {
        var query = @"select 
                        idparcelafinanciamento, idfinanciamento, numeroparcela, valorparcela, 
                        valorparcela, datavencimento, datapagamento
                        from ParcelaFinanciamento";    

        var parcelasfinanciamentos = await dbConnection.QueryAsync<ParcelaFinanciamentoEntity>(query);

        return parcelasfinanciamentos;
    }

    public async Task<ParcelaFinanciamentoEntity> RetornaParcelaFinanciamentoAsync(int idparcelafinanciamento)
    {
        var parcelafinanciamentoParams = new DynamicParameters();
        parcelafinanciamentoParams.Add("idparcelafinanciamento", idparcelafinanciamento);

        var query = @"select 
                        idparcelafinanciamento, idfinanciamento, numeroparcela, valorparcela, 
                        datavencimento, datapagamento
                        from ParcelaFinanciamento 
                        Where idparcelafinanciamento = @idparcelafinanciamento";

        var parcelafinanciamento = await dbConnection.QueryAsync<ParcelaFinanciamentoEntity>(query, parcelafinanciamentoParams);

        return parcelafinanciamento.SingleOrDefault();
    }
}