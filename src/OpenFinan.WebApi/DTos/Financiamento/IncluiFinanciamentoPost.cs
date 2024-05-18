using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using OpenFinan.Domain;

namespace OpenFinan.WebApi.Dtos.Financiamento;

public class IncluiFinanciamentoPost
{
    [Required]
    public int cpf  { get; set; }
    [Required]
    public int idtipofinanciamento { get; set; }
    [Required]
    public int quantidadeparcela { get; set; }
    [Required]
    public double valorcredito { get; set; }
    [Required]
    public DateTime dataprimeiraparcela { get; set; }
}