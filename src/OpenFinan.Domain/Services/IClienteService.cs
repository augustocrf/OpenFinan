using OpenFinan.Domain.Entity;

namespace OpenFinan.Domain.Services;

public interface IClienteService
{
    /// <summary>
    /// Verifica se um Cliente existe.
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns>True ou False</returns>
    Task<bool> ClienteExisteAsync(int cpf);

    /// <summary>
    /// retorna uma Cliente.
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns></returns>
    Task<ClienteEntity> RetornaClienteAsync(int cpf);

    /// <summary>
    /// Inclui uma nova Cliente
    /// </summary>
    /// <param name="cliente">Objeto Cliente</param>
    //// <exception cref="ValidationCoreException"></exception>
    Task IncluiClienteAsync(ClienteEntity cliente);        

    /// <summary>
    /// Atualiza uma Cliente
    /// </summary>
    /// <param name="cliente">Objeto Cliente</param>
    //// <exception cref="DomainBase.Exceptions.ValidationCoreException"></exception>
    Task AtualizaClienteAsync(ClienteEntity cliente);

    /// <summary>
    /// Exclui uma Cliente
    /// </summary>
    /// <param name="idCliente">Identificador cpf</param>
    /// <returns></returns>
    /// <exception cref="Exceptions.CategoriaCoreException"></exception>
    Task ExcluiClienteAsync(int cpf);

    /// <summary>
    /// Retorna uma lista de Clientes
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ClienteEntity>> RetornaClientesAsync();
}