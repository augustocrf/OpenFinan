using System;

namespace OpenFinan.Domain.Entity;

public class TipoFinanciamentoEntity
{
    public int idtipofinanciamento { get; set; }
    public string descricao {get; set;}
    public double taxa { get; set; }
}