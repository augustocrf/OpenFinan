using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Repositories;

public interface IClienteReadOnlyRepository
{		
    Task<ClienteEntity> RetornaClienteAsync(int cpf);		

    Task<bool> ClienteExisteAsync(int cpf);

    Task<IEnumerable<ClienteEntity>> ListaClientesAsync();
}