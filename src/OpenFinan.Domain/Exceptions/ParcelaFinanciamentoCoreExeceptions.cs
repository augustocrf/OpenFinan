using OpenFinan.DomainBase.Exceptions;

namespace OpenFinan.Domain.Exceptions;

public class ParcelaFinanciamentoCoreException : CoreException<ParcelaFinanciamentoCoreError>
{
    public ParcelaFinanciamentoCoreException() : base()	
    {
    }

    public ParcelaFinanciamentoCoreException(params ParcelaFinanciamentoCoreError[] errors) => AddError(errors);

    public override string Key => "ParcelaFinanciamentoCoreException";
}