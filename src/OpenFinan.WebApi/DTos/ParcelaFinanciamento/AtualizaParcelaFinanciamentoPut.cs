namespace OpenFinan.WebApi.Dtos.ParcelaFinanciamento;

public class AtualizaParcelaFinanciamentoPut
{
    public int idparcelafinanciamento {get; set;}
    public int numeroparcela { get; set; } 
    public double valorparcela { get; set; }
    public DateTime datavencimento { get; set; }
    public DateTime datapagamento { get; set; }
}