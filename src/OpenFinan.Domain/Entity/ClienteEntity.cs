using System;
namespace OpenFinan.Domain.Entity;

public class ClienteEntity
{
    public int cpf { get; set; }
    public string nome { get; set; }
    public string uf { get; set; }
    public string celular { get; set; }
}
