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

public class FinanciamentoServiceException : System.Exception
{
    public FinanciamentoServiceException() { }
    public FinanciamentoServiceException(string message) : base(message) { }
    public FinanciamentoServiceException(string message, System.Exception inner) : base(message, inner) { }
    protected FinanciamentoServiceException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}