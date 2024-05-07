namespace OpenFinan.WebApi.Dtos.Financiamento;

public class ResultadoFinanciamentoGet
{
    public int cpf  { get; set; }
    public string statuscredito { get; set; }
    public double valortotal { get; set; }
    public double valorjuros { get; set; }  
    public int quantidadeparcela { get; set; }
    public DateTime dataprimeiraparcela { get; set; }
}