using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Repositories;

public interface IClienteWriteOnlyRepository
{
	Task IncluiClienteAsync(ClienteEntity cliente);		

	Task AtualizaClienteAsync(ClienteEntity cliente);

	Task ExcluiClienteAsync(int cpf);
}