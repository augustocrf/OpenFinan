using OpenFinan.DomainBase.Exceptions;

namespace OpenFinan.Domain.Exceptions;

public class FinanciamentoCoreError : CoreError
{
    protected FinanciamentoCoreError(string key, string message) : base(key, message)
    {
    }

    public static readonly FinanciamentoCoreError FinanciamentoNaoEncontrado = new FinanciamentoCoreError("400.001", "Financiamento não encontrado.");
    public static readonly FinanciamentoCoreError FinanciamentoExistente = new FinanciamentoCoreError("400.002", "Financiamento já cadastrado.");
}