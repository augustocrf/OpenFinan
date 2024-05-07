using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Repositories;

public interface IFinanciamentoWriteOnlyRepository
{
    Task IncluiFinanciamentoAsync(FinanciamentoEntity financiamento);		

    Task AtualizaFinanciamentoAsync(FinanciamentoEntity financiamento);

    Task ExcluiFinanciamentoAsync(int idfinanciamento);

    Task ExcluiFinanciamentoClienteAsync(int cpf);
}