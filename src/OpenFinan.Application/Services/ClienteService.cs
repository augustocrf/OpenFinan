using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenFinan.Domain.Exceptions;
using OpenFinan.Domain.Entity;
using OpenFinan.Domain.Repositories;
using OpenFinan.Domain.Services;
using Microsoft.Extensions.Logging;
using Otc.Validations.Helpers;

namespace OpenFinan.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteReadOnlyRepository clienteReadOnlyRepository;
    private readonly IClienteWriteOnlyRepository clienteWriteOnlyRepository;
    private readonly ILogger logger;

    public ClienteService(IClienteReadOnlyRepository clienteReadOnly
        ,IClienteWriteOnlyRepository clienteWriteOnly
        ,ILoggerFactory loggerFactory)
    {
        this.clienteReadOnlyRepository = clienteReadOnly ?? throw new ArgumentNullException(nameof(clienteReadOnly));
        this.clienteWriteOnlyRepository = clienteWriteOnly ?? throw new ArgumentNullException(nameof(clienteWriteOnly));

        this.logger = loggerFactory?.CreateLogger<ClienteService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public async Task AtualizaClienteAsync(ClienteEntity cliente)
    {
        if (cliente == null)
            throw new ArgumentNullException(nameof(cliente));

        ValidationHelper.ThrowValidationExceptionIfNotValid(cliente);

        if (!await ClienteExisteAsync(cliente.cpf))
            throw new ClienteCoreException(ClienteCoreError.ClienteNaoEncontrado);

        await clienteWriteOnlyRepository.AtualizaClienteAsync(cliente);
    }

    public async Task<bool> ClienteExisteAsync(int cpf)
    {
        var result = await clienteReadOnlyRepository.ClienteExisteAsync(cpf);

        return result;
    }

    public async Task ExcluiClienteAsync(int cpf)
    {
        if (!await ClienteExisteAsync(cpf))
            throw new ClienteCoreException(ClienteCoreError.ClienteNaoEncontrado);

        await clienteWriteOnlyRepository.ExcluiClienteAsync(cpf);
    }

    public async Task IncluiClienteAsync(ClienteEntity cliente)
    {
        if (cliente == null)
            throw new ArgumentNullException(nameof(cliente));

        ValidationHelper.ThrowValidationExceptionIfNotValid(cliente);

        // Utilizar a gravação de logInformation somente se for realmente necessário
        // ter um acompanhamento de tudo que esta acontecendo
        logger.LogInformation("Iniciando gravação da Cliente...");
        await this.clienteWriteOnlyRepository.IncluiClienteAsync(cliente);
        logger.LogInformation("Cliente gravado.");
    }

    public async Task<ClienteEntity> RetornaClienteAsync(int cpf)
    {
        var cliente = await clienteReadOnlyRepository.RetornaClienteAsync(cpf);
        
        return cliente;
    }

    public async Task<IEnumerable<ClienteEntity>> RetornaClientesAsync()
    {
        var clientes = await clienteReadOnlyRepository.ListaClientesAsync();

        return clientes;
    }
}