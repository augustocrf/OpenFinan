using OpenFinan.DomainBase.Exceptions;

namespace OpenFinan.Domain.Exceptions;

public class ParcelaFinanciamentoCoreError : CoreError
{
    protected ParcelaFinanciamentoCoreError(string key, string message) : base(key, message)
    {
    }

    public static readonly ParcelaFinanciamentoCoreError ParcelaFinanciamentoNaoEncontrado = new ParcelaFinanciamentoCoreError("400.001", "Parcela do Financiamento não encontrado.");
    public static readonly ParcelaFinanciamentoCoreError ParcelaFinanciamentoExistente = new ParcelaFinanciamentoCoreError("400.002", "Parcela do Financiamento já cadastrado.");
}