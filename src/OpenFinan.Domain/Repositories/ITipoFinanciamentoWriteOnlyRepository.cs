using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Repositories;

public interface ITipoFinanciamentoWriteOnlyRepository
{
    Task IncluiTipoFinanciamentoAsync(TipoFinanciamentoEntity TipoFinanciamento);		

    Task AtualizaTipoFinanciamentoAsync(TipoFinanciamentoEntity TipoFinanciamento);

    Task ExcluiTipoFinanciamentoAsync(int idTipoFinanciamento);
}