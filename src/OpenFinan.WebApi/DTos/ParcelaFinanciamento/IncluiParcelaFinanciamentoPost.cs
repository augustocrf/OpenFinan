namespace OpenFinan.WebApi.Dtos.ParcelaFinanciamento;

public class IncluiParcelaFinanciamentoPost
{
    public int idfinanciamento { get; set; }
    public int numeroparcela { get; set; } 
    public double valorparcela { get; set; }
    public DateTime datavencimento { get; set; }
}