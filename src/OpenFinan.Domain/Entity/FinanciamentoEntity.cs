using System;

namespace OpenFinan.Domain.Entity;

public class FinanciamentoEntity
{
    public int idfinanciamento {get; set;}
    public int cpf {get; set;}
    public int idtipofinanciamento { get; set; }
    public double valortotal { get; set; }
    public DateTime dataultimovencimento { get; set; }
}