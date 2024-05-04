using System;

namespace OpenFinan.Domain.Entity;

public class ParcelaFinanciamentoEntity
{
    public int idparcelafinanciamento {get; set;}
    public int idfinanciamento { get; set; }
    public int numeroparcela { get; set; } 
    public double valorparcela { get; set; }
    public DateTime datavencimento { get; set; }
    public DateTime datapamento { get; set; }
}