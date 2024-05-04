namespace OpenFinan.WebApi.Dtos.TipoFinanciamento;

public class IncluiTipoFinanciamentoPost
{
    public int idTipoFinanciamento { get; set; }
    public string descricao { get; set; }
    public string taxa { get; set; }
}