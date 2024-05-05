namespace OpenFinan.WebApi.Dtos.Financiamento;

public class AtualizaFinanciamentoPut
{
    public int idfinanciamento { get; set; }
    public int idtipofinanciamento { get; set; }
    public double valortotal { get; set; }
    public DateTime dataultimovencimento { get; set; }
}