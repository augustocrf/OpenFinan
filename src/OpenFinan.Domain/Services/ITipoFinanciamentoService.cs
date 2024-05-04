using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Services;

public interface ITipoFinanciamentoService
{
    /// <summary>
    /// Verifica se uma TipoFinanciamento existe.
    /// </summary>
    /// <param name="idTipoFinanciamento></param>
    /// <returns>True ou False</returns>
    Task<bool> TipoFinanciamentoExisteAsync(int idTipoFinanciamento);

    /// <summary>
    /// retorna uma TipoFinanciamento.
    /// </summary>
    /// <param name="idTipoFinanciamento"></param>
    /// <returns></returns>
    Task<TipoFinanciamentoEntity> RetornaTipoFinanciamentoAsync(int idTipoFinanciamento);

    /// <summary>
    /// Inclui uma nova TipoFinanciamento
    /// </summary>
    /// <param name="TipoFinanciamento">TipoFinanciamento Categoria</param>
    //// <exception cref="ValidationCoreException"></exception>
    Task IncluiTipoFinanciamentoAsync(TipoFinanciamentoEntity tipofinanciamento);        

    /// <summary>
    /// Atualiza uma TipoFinanciamento
    /// </summary>
    /// <param name="TipoFinanciamento">Objeto TipoFinanciamento</param>
    //// <exception cref="DomainBase.Exceptions.ValidationCoreException"></exception>
    Task AtualizaTipoFinanciamentosync(TipoFinanciamentoEntity TipoFinanciamento);

    /// <summary>
    /// Exclui uma TipoFinanciamento
    /// </summary>
    /// <param name="idTipoFinanciamento">Identificador</param>
    /// <returns></returns>
    /// <exception cref="Exceptions.TipoFinanciamentoCoreException"></exception>
    Task ExcluiTipoFinanciamentoAsync(int idTipoFinanciamento);

    /// <summary>
    /// Retorna uma lista de TipoFinanciamentos
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<TipoFinanciamentoEntity>> RetornaTipoFinanciamentosAsync();
}