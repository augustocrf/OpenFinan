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

public class TipoFinanciamentoService : ITipoFinanciamentoService
{
    private readonly ITipoFinanciamentoReadOnlyRepository tipofinanciamentoReadOnlyRepository;
    private readonly ITipoFinanciamentoWriteOnlyRepository tipofinanciamentoWriteOnlyRepository;
    private readonly ILogger logger;

    public TipoFinanciamentoService(ITipoFinanciamentoReadOnlyRepository tipofinanciamentoReadOnlyRepository
        ,ITipoFinanciamentoWriteOnlyRepository tipofinanciamentoWriteOnlyRepository
        ,ILoggerFactory loggerFactory)
    {
        this.tipofinanciamentoReadOnlyRepository  = tipofinanciamentoReadOnlyRepository ?? throw new ArgumentNullException(nameof(tipofinanciamentoReadOnlyRepository));
        this.tipofinanciamentoWriteOnlyRepository  = tipofinanciamentoWriteOnlyRepository ?? throw new ArgumentNullException(nameof(tipofinanciamentoWriteOnlyRepository));

        this.logger = loggerFactory?.CreateLogger<TipoFinanciamentoService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public async Task AtualizaTipoFinanciamentoAsync(TipoFinanciamentoEntity tipofinanciamento)
    {
        if (tipofinanciamento == null)
            throw new ArgumentNullException(nameof(tipofinanciamento));

        ValidationHelper.ThrowValidationExceptionIfNotValid(tipofinanciamento);

        if (!await TipoFinanciamentoExisteAsync(tipofinanciamento.idtipofinanciamento))
            throw new TipoFinanciamentoCoreException(TipoFinanciamentoCoreError.TipoFinanciamentoNaoEncontrado);

        await tipofinanciamentoWriteOnlyRepository.AtualizaTipoFinanciamentoAsync(tipofinanciamento);
    }

    public async Task<bool> TipoFinanciamentoExisteAsync(int idtipofinanciamento)
    {
        var result = await tipofinanciamentoReadOnlyRepository.TipoFinanciamentoExisteAsync(idtipofinanciamento);

        return result;
    }

    public async Task ExcluiTipoFinanciamentoAsync(int idtipofinanciamento)
    {
        if (!await TipoFinanciamentoExisteAsync(idtipofinanciamento))
            throw new TipoFinanciamentoCoreException(TipoFinanciamentoCoreError.TipoFinanciamentoNaoEncontrado);

        await tipofinanciamentoWriteOnlyRepository.ExcluiTipoFinanciamentoAsync(idtipofinanciamento);
    }

    public async Task IncluiTipoFinanciamentoAsync(TipoFinanciamentoEntity tipofinanciamento)
    {
        if (tipofinanciamento == null)
            throw new ArgumentNullException(nameof(tipofinanciamento));

        ValidationHelper.ThrowValidationExceptionIfNotValid(tipofinanciamento);

        // Utilizar a gravação de logInformation somente se for realmente necessário
        // ter um acompanhamento de tudo que esta acontecendo
        logger.LogInformation("Iniciando gravação da Tipo Financiamento...");
        await this.tipofinanciamentoWriteOnlyRepository.IncluiTipoFinanciamentoAsync(tipofinanciamento);
        logger.LogInformation("Tipo Financiamento gravado.");
    }

    public async Task<TipoFinanciamentoEntity> RetornaTipoFinanciamentoAsync(int idtipofinanciamento)
    {
        var tipofinanciamento = await tipofinanciamentoReadOnlyRepository.RetornaTipoFinanciamentoAsync(idtipofinanciamento);
        
        return tipofinanciamento;
    }

    public async Task<IEnumerable<TipoFinanciamentoEntity>> RetornaTipoFinanciamentosAsync()
    {
        var tipofinanciamentos = await tipofinanciamentoReadOnlyRepository.ListaTipoFinanciamentosAsync();

        return tipofinanciamentos;
    }
}