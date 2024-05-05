namespace OpenFinan.WebApi.Dtos.ParcelaFinanciamento;

public class RetornaParcelaFinanciamentoGet
{
    public int idparcelafinanciamento { get; set; }
    public int idfinanciamento { get; set; }
    public int numeroparcela { get; set; } 
    public double valorparcela { get; set; }
    public DateTime datavencimento { get; set; }
    public DateTime datapagamento { get; set; }
}