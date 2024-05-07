using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Repositories;

public interface IFinanciamentoReadOnlyRepository
{
    Task<FinanciamentoEntity> RetornaFinanciamentoAsync(int idFinanciamento);

    Task<bool> FinanciamentoExisteAsync(int idFinanciamento);

    Task<bool> FinanciamentoClienteExisteAsync(int cpf);

    Task<IEnumerable<FinanciamentoEntity>> ListaFinanciamentosAsync();
}