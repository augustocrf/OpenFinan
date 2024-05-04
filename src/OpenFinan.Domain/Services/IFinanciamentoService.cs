using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Services;

public interface IFinanciamentoService
{
    /// <summary>
    /// Verifica se uma Financiamento existe.
    /// </summary>
    /// <param name="idFinanciamento"></param>
    /// <returns>True ou False</returns>
    Task<bool> FinanciamentoExisteAsync(int idFinanciamento);

    /// <summary>
    /// Verifica se o cliente possui Financiamento existe.
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns>True ou False</returns>
    Task<bool> FinanciamentoClienteExisteAsync(int cpf);

    /// <summary>
    /// retorna uma Financiamento.
    /// </summary>
    /// <param name="idFinanciamento"></param>
    /// <returns></returns>
    Task<FinanciamentoEntity> RetornaFinanciamentoAsync(int idFinanciamento);

    /// <summary>
    /// Inclui uma nova Financiamento
    /// </summary>
    /// <param name="financiamento">Objeto Financiamento</param>
    //// <exception cref="ValidationCoreException"></exception>
    Task IncluiFinanciamentoAsync(FinanciamentoEntity financiamento);        

    /// <summary>
    /// Atualiza uma Financiamento
    /// </summary>
    /// <param name="financiamento">Objeto Financiamento</param>
    //// <exception cref="DomainBase.Exceptions.ValidationCoreException"></exception>
    Task AtualizaFinanciamentoAsync(FinanciamentoEntity financiamento);

    /// <summary>
    /// Exclui uma Financiamento
    /// </summary>
    /// <param name="idfinanciamento">Identificador</param>
    /// <returns></returns>
    /// <exception cref="Exceptions.FinanciamentoCoreException"></exception>
    Task ExcluiFinanciamentoAsync(int idfinanciamento);

    /// <summary>
    /// Exclui todos os Financiamento do Cliente
    /// </summary>
    /// <param name="cpf">Identificador</param>
    /// <returns></returns>
    /// <exception cref="Exceptions.FinanciamentoCoreException"></exception>
    Task ExcluiFinanciamentoClienteAsync(int cpf);

    /// <summary>
    /// Retorna uma lista de Financiamentos
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<FinanciamentoEntity>> RetornaFinanciamentosAsync();
}