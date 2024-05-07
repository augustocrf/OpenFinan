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

public class FinanciamentoService : IFinanciamentoService
{
    private readonly IFinanciamentoReadOnlyRepository financiamentoReadOnlyRepository;
    private readonly IFinanciamentoWriteOnlyRepository financiamentoWriteOnlyRepository;
    private readonly ILogger logger;

    public FinanciamentoService(IFinanciamentoReadOnlyRepository financiamentoReadOnlyRepository
        ,IFinanciamentoWriteOnlyRepository financiamentoWriteOnlyRepository
        ,ILoggerFactory loggerFactory)
    {
        this.financiamentoReadOnlyRepository  = financiamentoReadOnlyRepository ?? throw new ArgumentNullException(nameof(financiamentoReadOnlyRepository));
        this.financiamentoWriteOnlyRepository  = financiamentoWriteOnlyRepository ?? throw new ArgumentNullException(nameof(financiamentoWriteOnlyRepository));

        this.logger = loggerFactory?.CreateLogger<FinanciamentoService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public async Task AtualizaFinanciamentoAsync(FinanciamentoEntity financiamento)
    {
        if (financiamento == null)
            throw new ArgumentNullException(nameof(financiamento));

        ValidationHelper.ThrowValidationExceptionIfNotValid(financiamento);

        if (!await FinanciamentoExisteAsync(financiamento.idfinanciamento))
            throw new FinanciamentoCoreException(FinanciamentoCoreError.FinanciamentoNaoEncontrado);

        await financiamentoWriteOnlyRepository.AtualizaFinanciamentoAsync(financiamento);
    }

    public async Task<bool> FinanciamentoExisteAsync(int idfinanciamento)
    {
        var result = await financiamentoReadOnlyRepository.FinanciamentoExisteAsync(idfinanciamento);

        return result;
    }

    public async Task<bool> FinanciamentoClienteExisteAsync(int cpf)
    {
        var result = await financiamentoReadOnlyRepository.FinanciamentoClienteExisteAsync(cpf);

        return result;
    }

    public async Task ExcluiFinanciamentoAsync(int idfinanciamento)
    {
        if (!await FinanciamentoExisteAsync(idfinanciamento))
            throw new FinanciamentoCoreException(FinanciamentoCoreError.FinanciamentoNaoEncontrado);

        await financiamentoWriteOnlyRepository.ExcluiFinanciamentoAsync(idfinanciamento);
    }

    public async Task ExcluiFinanciamentoClienteAsync(int cpf)
    {
        if (!await FinanciamentoClienteExisteAsync(cpf))
            throw new FinanciamentoCoreException(FinanciamentoCoreError.FinanciamentoNaoEncontrado);

        await financiamentoWriteOnlyRepository.ExcluiFinanciamentoClienteAsync(cpf);
    }

    public async Task IncluiFinanciamentoAsync(FinanciamentoEntity financiamento)
    {
        if (financiamento == null)
            throw new ArgumentNullException(nameof(financiamento));

        ValidationHelper.ThrowValidationExceptionIfNotValid(financiamento);

        //3 - Credito Pesssoa Juridica
        if(financiamento.idtipofinanciamento == 3) 
            if (financiamento.valorcredito < 15000)
                throw new FinanciamentoServiceException("Para o crédito pessoa juridica o valorcredito tem que ser maior que R$ 15000,00.");
                
        // Utilizar a gravação de logInformation somente se for realmente necessário
        // ter um acompanhamento de tudo que esta acontecendo
        logger.LogInformation("Iniciando gravação da Financiamento...");
        await this.financiamentoWriteOnlyRepository.IncluiFinanciamentoAsync(financiamento);
        logger.LogInformation("Financiamento gravado.");
    }

    public async Task<FinanciamentoEntity> RetornaFinanciamentoAsync(int idfinanciamento)
    {
        var financiamento = await financiamentoReadOnlyRepository.RetornaFinanciamentoAsync(idfinanciamento);
        
        return financiamento;
    }

    public async Task<IEnumerable<FinanciamentoEntity>> RetornaFinanciamentosAsync()
    {
        var financiamentos = await financiamentoReadOnlyRepository.ListaFinanciamentosAsync();

        return financiamentos;
    }
}