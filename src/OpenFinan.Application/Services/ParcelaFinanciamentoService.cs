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

public class ParcelaFinanciamentoService : IParcelaFinanciamentoService
{
    private readonly IParcelaFinanciamentoReadOnlyRepository parcelafinanciamentoReadOnlyRepository;
    private readonly IParcelaFinanciamentoWriteOnlyRepository parcelafinanciamentoWriteOnlyRepository;
    private readonly ILogger logger;

    public ParcelaFinanciamentoService(IParcelaFinanciamentoReadOnlyRepository parcelafinanciamentoReadOnlyRepository
        ,IParcelaFinanciamentoWriteOnlyRepository parcelafinanciamentoWriteOnlyRepository
        ,ILoggerFactory loggerFactory)
    {
        this.parcelafinanciamentoReadOnlyRepository  = parcelafinanciamentoReadOnlyRepository ?? throw new ArgumentNullException(nameof(parcelafinanciamentoReadOnlyRepository));
        this.parcelafinanciamentoWriteOnlyRepository  = parcelafinanciamentoWriteOnlyRepository ?? throw new ArgumentNullException(nameof(parcelafinanciamentoWriteOnlyRepository));

        this.logger = loggerFactory?.CreateLogger<ParcelaFinanciamentoService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public async Task AtualizaParcelaFinanciamentoAsync(ParcelaFinanciamentoEntity parcelafinanciamento)
    {
        if (parcelafinanciamento == null)
            throw new ArgumentNullException(nameof(parcelafinanciamento));

        ValidationHelper.ThrowValidationExceptionIfNotValid(parcelafinanciamento);

        if (!await ParcelaFinanciamentoExisteAsync(parcelafinanciamento.idparcelafinanciamento))
            throw new ParcelaFinanciamentoCoreException(ParcelaFinanciamentoCoreError.ParcelaFinanciamentoNaoEncontrado);

        await parcelafinanciamentoWriteOnlyRepository.AtualizaParcelaFinanciamentoAsync(parcelafinanciamento);
    }

    public async Task<bool> ParcelaFinanciamentoExisteAsync(int idparcelafinanciamento)
    {
        var result = await parcelafinanciamentoReadOnlyRepository.ParcelaFinanciamentoExisteAsync(idparcelafinanciamento);

        return result;
    }

    public async Task<bool> ParcelasFinanciamentoExisteAsync(int idfinanciamento)
    {
        var result = await parcelafinanciamentoReadOnlyRepository.ParcelasFinanciamentoExisteAsync(idfinanciamento);

        return result;
    }

    public async Task ExcluiParcelaFinanciamentoAsync(int idparcelafinanciamento)
    {
        if (!await ParcelaFinanciamentoExisteAsync(idparcelafinanciamento))
            throw new ParcelaFinanciamentoCoreException(ParcelaFinanciamentoCoreError.ParcelaFinanciamentoNaoEncontrado);

        await parcelafinanciamentoWriteOnlyRepository.ExcluiParcelaFinanciamentoAsync(idparcelafinanciamento);
    }

    public async Task ExcluiParcelasFinanciamentoAsync(int idfinanciamento)
    {
        if (!await ParcelasFinanciamentoExisteAsync(idfinanciamento))
            throw new ParcelaFinanciamentoCoreException(ParcelaFinanciamentoCoreError.ParcelaFinanciamentoNaoEncontrado);

        await parcelafinanciamentoWriteOnlyRepository.ExcluiParcelasFinanciamentoAsync(idfinanciamento);
    }

    public async Task IncluiParcelaFinanciamentoAsync(ParcelaFinanciamentoEntity parcelafinanciamento)
    {
        if (parcelafinanciamento == null)
            throw new ArgumentNullException(nameof(parcelafinanciamento));

        ValidationHelper.ThrowValidationExceptionIfNotValid(parcelafinanciamento);

        // Utilizar a gravação de logInformation somente se for realmente necessário
        // ter um acompanhamento de tudo que esta acontecendo
        logger.LogInformation("Iniciando gravação da Parcela Financiamento...");
        await this.parcelafinanciamentoWriteOnlyRepository.IncluiParcelaFinanciamentoAsync(parcelafinanciamento);
        logger.LogInformation("Parcela Financiamento gravado.");
    }

    public async Task<ParcelaFinanciamentoEntity> RetornaParcelaFinanciamentoAsync(int idparcelafinanciamento)
    {
        var parcelafinanciamento = await parcelafinanciamentoReadOnlyRepository.RetornaParcelaFinanciamentoAsync(idparcelafinanciamento);
        
        return parcelafinanciamento;
    }

    public async Task<IEnumerable<ParcelaFinanciamentoEntity>> RetornaParcelasFinanciamentosAsync()
    {
        var parcelasfinanciamentos = await parcelafinanciamentoReadOnlyRepository.ListaParcelasFinanciamentosAsync();

        return parcelasfinanciamentos;
    }
}