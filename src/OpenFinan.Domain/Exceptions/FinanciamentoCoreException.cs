using OpenFinan.DomainBase.Exceptions;

namespace OpenFinan.Domain.Exceptions;

public class FinanciamentoCoreException : CoreException<FinanciamentoCoreError>
{
    public FinanciamentoCoreException() : base()	
    {
    }

    public FinanciamentoCoreException(params FinanciamentoCoreError[] errors) => AddError(errors);

    public override string Key => "FinanciamentoCoreException";
}