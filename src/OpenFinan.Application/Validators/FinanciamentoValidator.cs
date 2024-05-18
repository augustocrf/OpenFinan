
using OpenFinan.Domain.Entity;
using OpenFinan.Domain.Validators;

namespace OpenFinan.Application.Validators;


public class FinanciamentoValidator : IFinanciamentoValidator
{
    public bool ValidateAsync(FinanciamentoEntity financiamento)
    {
        return ValidateValorCreditoAsync(financiamento) &&
                ValidateQuantidadeParcelaAsync(financiamento) &&
                ValidateTipoFinanciamentoAsync(financiamento) &&
                ValidateDataPrimeiraParcelaAsync(financiamento);
    }
    private bool  ValidateValorCreditoAsync(FinanciamentoEntity  financiamento)
    {
        return financiamento.valorcredito > 0 && financiamento.valorcredito <= 1000000;
    }
    private bool ValidateQuantidadeParcelaAsync(FinanciamentoEntity  financiamento)
    {
        return financiamento.quantidadeparcela >= 5 && financiamento.quantidadeparcela <= 72;
    }
    private bool ValidateTipoFinanciamentoAsync(FinanciamentoEntity  financiamento)
    {
        return financiamento.idtipofinanciamento !=  3 || financiamento.valorcredito >= 15000;
    }   
    private bool ValidateDataPrimeiraParcelaAsync(FinanciamentoEntity  financiamento)
    {
        var dataPeriodoInicial = financiamento.dataprimeiraparcela.AddDays(5);
        var dataPeriodoFinal = financiamento.dataprimeiraparcela.AddDays(40);

        return financiamento.dataprimeiraparcela > dataPeriodoInicial && financiamento.dataprimeiraparcela <= dataPeriodoFinal;
    } 
}