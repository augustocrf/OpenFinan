namespace OpenFinan.WebApi.Dtos.Financiamento;

public class IncluiFinanciamentoPost
{
    public int idfinanciamento { get; set; }
    public int cpf  { get; set; }
    public int idtipofinanciamento { get; set; }
    public double valortotal { get; set; }
    public DateTime dataultimovencimento { get; set; }
}