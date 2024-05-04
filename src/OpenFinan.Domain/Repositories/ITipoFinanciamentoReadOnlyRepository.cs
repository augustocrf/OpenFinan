using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Repositories;

public interface ITipoFinanciamentoReadOnlyRepository
{
    Task<TipoFinanciamentoEntity> RetornaTipoFinanciamentoAsync(int idTipoFinanciamento);

    Task<bool> TipoFinanciamentoExisteAsync(int idTipoFinanciamento);

    Task<IEnumerable<TipoFinanciamentoEntity>> ListaTipoFinanciamentosAsync();
}