using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Validators;

public interface IFinanciamentoValidator
{
    bool ValidateAsync(FinanciamentoEntity financiamento);
}