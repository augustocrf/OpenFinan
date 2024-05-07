using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Services;

public interface IParcelaFinanciamentoService
{
    /// <summary>
    /// Verifica se uma Parcela Financiamento existe.
    /// </summary>
    /// <param name="idparcelafinanciamento"></param>
    /// <returns>True ou False</returns>
    Task<bool> ParcelaFinanciamentoExisteAsync(int idparcelafinanciamento);

    /// <summary>
    /// Verifica se o financiamento possui Parcelas existente.
    /// </summary>
    /// <param name="idfinanciamento"></param>
    /// <returns>True ou False</returns>
    Task<bool> ParcelasFinanciamentoExisteAsync(int idfinanciamento);

    /// <summary>
    /// retorna uma parcela do Financiamento.
    /// </summary>
    /// <param name="idparcelafinanciamento"></param>
    /// <returns></returns>
    Task<ParcelaFinanciamentoEntity> RetornaParcelaFinanciamentoAsync(int idparcelafinanciamento);

    /// <summary>
    /// Inclui uma nova Parcela Financiamento
    /// </summary>
    /// <param name="parcelafinanciamento">Objeto ParcelaFinanciamento</param>
    //// <exception cref="ValidationCoreException"></exception>
    Task IncluiParcelaFinanciamentoAsync(ParcelaFinanciamentoEntity parcelafinanciamento);        

    /// <summary>
    /// Atualiza uma ParcelaFinanciamento
    /// </summary>
    /// <param name="parcelafinanciamento">Objeto ParcelaFinanciamento</param>
    //// <exception cref="DomainBase.Exceptions.ValidationCoreException"></exception>
    Task AtualizaParcelaFinanciamentoAsync(ParcelaFinanciamentoEntity parcelafinanciamento);

    /// <summary>
    /// Exclui uma ParcelaFinanciamento
    /// </summary>
    /// <param name="idparcelafinanciamento">Identificador</param>
    /// <returns></returns>
    /// <exception cref="Exceptions.ParcelaFinanciamentoCoreException"></exception>
    Task ExcluiParcelaFinanciamentoAsync(int idparcelafinanciamento);

    /// <summary>
    /// Exclui todas as parcelas de um Financiamento
    /// </summary>
    /// <param name="idfinanciamento">Identificador</param>
    /// <returns></returns>
    /// <exception cref="Exceptions.FinanciamentoCoreException"></exception>
    Task ExcluiParcelasFinanciamentoAsync(int idfinanciamento);

    /// <summary>
    /// Retorna uma lista de ParcelasFinanciamentos
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ParcelaFinanciamentoEntity>> RetornaParcelasFinanciamentosAsync();
}