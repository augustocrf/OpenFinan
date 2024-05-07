using OpenFinan.DomainBase.Exceptions;

namespace OpenFinan.Domain.Exceptions;

public class TipoFinanciamentoCoreError : CoreError
{
    protected TipoFinanciamentoCoreError(string key, string message) : base(key, message)
    {
    }

    public static readonly TipoFinanciamentoCoreError TipoFinanciamentoNaoEncontrado = new TipoFinanciamentoCoreError("400.001", "TipoFinanciamento não encontrado.");
    public static readonly TipoFinanciamentoCoreError TipoFinanciamentoExistente = new TipoFinanciamentoCoreError("400.002", "TipoFinanciamento já cadastrado.");
}