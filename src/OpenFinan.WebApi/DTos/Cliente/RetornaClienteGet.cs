namespace OpenFinan.WebApi.Dtos.Cliente;

public class RetornaClienteGet
{
    public int cpf { get; set; }
    public string nome { get; set; }
    public string uf { get; set; }
    public string celular { get; set; }
}