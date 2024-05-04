using OpenFinan.DomainBase.Exceptions;

namespace OpenFinan.Domain.Exceptions;

public class ClienteCoreException : CoreException<ClienteCoreError>
{
    public ClienteCoreException() : base()	
    {
    }

    public ClienteCoreException(params ClienteCoreError[] errors) => AddError(errors);

    public override string Key => "ClienteCoreException";
}