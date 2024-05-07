using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Repositories;

public interface IParcelaFinanciamentoWriteOnlyRepository
{
    Task IncluiParcelaFinanciamentoAsync(ParcelaFinanciamentoEntity parcelafinanciamento);		

    Task AtualizaParcelaFinanciamentoAsync(ParcelaFinanciamentoEntity parcelafinanciamento);

    Task ExcluiParcelaFinanciamentoAsync(int idparcelafinanciamento);

    Task ExcluiParcelasFinanciamentoAsync(int idfinanciamento);
}