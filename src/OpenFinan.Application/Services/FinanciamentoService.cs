using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenFinan.Domain.Exceptions;
using OpenFinan.Domain.Entity;
using OpenFinan.Domain.Dtos;
using OpenFinan.Domain.Repositories;
using OpenFinan.Domain.Services;
using OpenFinan.Domain.Validators;
using Microsoft.Extensions.Logging;
using Otc.Validations.Helpers;
using System.Net;

namespace OpenFinan.Application.Services;

public class FinanciamentoService : IFinanciamentoService
{
    private readonly IFinanciamentoReadOnlyRepository financiamentoReadOnlyRepository;
    private readonly IFinanciamentoWriteOnlyRepository financiamentoWriteOnlyRepository;
    private readonly IParcelaFinanciamentoReadOnlyRepository parcelafinanciamentoReadOnlyRepository;
    private readonly IParcelaFinanciamentoWriteOnlyRepository parcelafinanciamentoWriteOnlyRepository;
    private readonly ITipoFinanciamentoReadOnlyRepository tipofinanciamentoReadOnlyRepository;
    private readonly ITipoFinanciamentoWriteOnlyRepository tipofinanciamentoWriteOnlyRepository;
    private readonly IClienteReadOnlyRepository clienteReadOnlyRepository;
    private readonly IClienteWriteOnlyRepository clienteWriteOnlyRepository;    
    private readonly IFinanciamentoValidator financiamentoValidator;
    private readonly ILogger logger;

    public FinanciamentoService(IFinanciamentoReadOnlyRepository financiamentoReadOnlyRepository
        ,IFinanciamentoWriteOnlyRepository financiamentoWriteOnlyRepository
        ,IParcelaFinanciamentoReadOnlyRepository parcelafinanciamentoReadOnlyRepository
        ,IParcelaFinanciamentoWriteOnlyRepository parcelafinanciamentoWriteOnlyRepository
        ,ITipoFinanciamentoReadOnlyRepository tipofinanciamentoReadOnlyRepository
        ,ITipoFinanciamentoWriteOnlyRepository tipofinanciamentoWriteOnlyRepository
        ,IClienteReadOnlyRepository clienteReadOnlyRepository
        ,IClienteWriteOnlyRepository clienteWriteOnlyRepository
        ,IFinanciamentoValidator financiamentoValidator
        ,ILoggerFactory loggerFactory)
    {
        this.financiamentoReadOnlyRepository  = financiamentoReadOnlyRepository ?? throw new ArgumentNullException(nameof(financiamentoReadOnlyRepository));
        this.financiamentoWriteOnlyRepository  = financiamentoWriteOnlyRepository ?? throw new ArgumentNullException(nameof(financiamentoWriteOnlyRepository));
        this.parcelafinanciamentoReadOnlyRepository  = parcelafinanciamentoReadOnlyRepository ?? throw new ArgumentNullException(nameof(parcelafinanciamentoReadOnlyRepository));
        this.parcelafinanciamentoWriteOnlyRepository  = parcelafinanciamentoWriteOnlyRepository ?? throw new ArgumentNullException(nameof(parcelafinanciamentoWriteOnlyRepository));
        this.tipofinanciamentoReadOnlyRepository  = tipofinanciamentoReadOnlyRepository ?? throw new ArgumentNullException(nameof(tipofinanciamentoReadOnlyRepository));
        this.tipofinanciamentoWriteOnlyRepository  = tipofinanciamentoWriteOnlyRepository ?? throw new ArgumentNullException(nameof(tipofinanciamentoWriteOnlyRepository));
        this.clienteReadOnlyRepository  = clienteReadOnlyRepository?? throw new ArgumentNullException(nameof(clienteReadOnlyRepository));
        this.clienteWriteOnlyRepository  = clienteWriteOnlyRepository?? throw new ArgumentNullException(nameof(clienteWriteOnlyRepository));
        this.financiamentoValidator  = financiamentoValidator?? throw new ArgumentNullException(nameof(financiamentoValidator));

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
    public async Task<FinanciamentoResult> IncluiFinanciamentoAsync(FinanciamentoEntity financiamento)
    {
        if (financiamento == null)
            return new FinanciamentoResult { status = "Recusado", valorTotal = 0, valorJuros = 0 };
            
        ValidationHelper.ThrowValidationExceptionIfNotValid(financiamento);

        // Pesquisa se o cliente exite
        ClienteEntity cliente = await this.clienteReadOnlyRepository.RetornaClienteAsync(financiamento.cpf);
        if (cliente == null)
            return new FinanciamentoResult { status = "Recusado", valorTotal = 0, valorJuros = 0 };

        // Pesquisa se o tipo de financiamento exite
        TipoFinanciamentoEntity tipofinanciamento = await this.tipofinanciamentoReadOnlyRepository.RetornaTipoFinanciamentoAsync(financiamento.idtipofinanciamento);
        if (tipofinanciamento == null)
            return new FinanciamentoResult { status = "Recusado", valorTotal = 0, valorJuros = 0 };

        // Valida se o financiamento é valido
        if (!financiamentoValidator.ValidateAsync(financiamento))
            return new FinanciamentoResult { status = "Recusado", valorTotal = 0, valorJuros = 0 };

        // Utilizar a gravação de logInformation somente se for realmente necessário
        // ter um acompanhamento de tudo que esta acontecendo
        logger.LogInformation("Iniciando gravação da Financiamento...");
        await this.CalcularValorTotal(financiamento, tipofinanciamento.taxa);
        await this.financiamentoWriteOnlyRepository.IncluiFinanciamentoAsync(financiamento);
        logger.LogInformation("Financiamento gravado.");
        
        // Efetua o calculo da parcela e inclui as parcelas
        logger.LogInformation("Iniciando gravação da Parcela do Financiamento..."); 
        for (int index = 0; 1 < financiamento.quantidadeparcela; index++){
            ParcelaFinanciamentoEntity parcelafinanciamento = await CalcularParcela(financiamento, index);

            await this.parcelafinanciamentoWriteOnlyRepository.IncluiParcelaFinanciamentoAsync(parcelafinanciamento); 
        }       
        logger.LogInformation("Parcela Financiamento gravado.");

        return new FinanciamentoResult { status = "Recusado", valorTotal = (decimal)financiamento.valortotal, valorJuros = (decimal)financiamento.valorjuros}; 
    }
    private async Task CalcularValorTotal(FinanciamentoEntity financiamento, double taxa)
    {
        //Calcula o valor total de um financiamento com juros simples ao mës
        financiamento.valorjuros = ( financiamento.valorcredito * (taxa /100) * financiamento.quantidadeparcela );
        financiamento.valortotal = financiamento.valorcredito + financiamento.valorjuros;
        financiamento.dataultimovencimento = financiamento.dataprimeiraparcela.AddMonths(financiamento.quantidadeparcela);
    }

    private async Task<ParcelaFinanciamentoEntity> CalcularParcela(FinanciamentoEntity financiamento, int index){
        ParcelaFinanciamentoEntity parcelafinanciamento = new ParcelaFinanciamentoEntity();
        
        parcelafinanciamento.idfinanciamento = financiamento.idfinanciamento;
        parcelafinanciamento.numeroparcela = index+1;
        parcelafinanciamento.valorparcela = (financiamento.valortotal / financiamento.quantidadeparcela);
        parcelafinanciamento.datavencimento = financiamento.dataprimeiraparcela.AddMonths(index);

        return parcelafinanciamento;
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