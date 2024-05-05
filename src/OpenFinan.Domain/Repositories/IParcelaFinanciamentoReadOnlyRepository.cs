using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Repositories;

public interface IParcelaFinanciamentoReadOnlyRepository
{
    Task<ParcelaFinanciamentoEntity> RetornaParcelaFinanciamentoAsync(int idParcelaFinanciamento);

    Task<bool> ParcelaFinanciamentoExisteAsync(int idParcelaFinanciamento);

    Task<bool> ParcelasFinanciamentoExisteAsync(int idFinanciamento);

    Task<IEnumerable<ParcelaFinanciamentoEntity>> ListaParcelasFinanciamentosAsync();
}