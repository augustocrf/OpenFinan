using OpenFinan.DomainBase.Exceptions;

namespace OpenFinan.Domain.Exceptions;

public class TipoFinanciamentoCoreException : CoreException<TipoFinanciamentoCoreError>
{
    public TipoFinanciamentoCoreException() : base()	
    {
    }

    public TipoFinanciamentoCoreException(params TipoFinanciamentoCoreError[] errors) => AddError(errors);

    public override string Key => "TipoFinanciamentoCoreException";
}