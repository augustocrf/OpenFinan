namespace OpenFinan.Domain.Repositories;

public class IClienteReadOnlyRepository
{		
        Task<ClienteEntity> RetornaClienteAsync(int cpf);		

		Task<bool> ClienteExisteAsync(int cpf);

		Task<IEnumerable<ClienteEntity>> ListaClienteAsync();
}