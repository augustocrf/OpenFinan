using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using OpenFinan.Domain;

namespace OpenFinan.WebApi.Dtos.Financiamento;

public class IncluiFinanciamentoPost
{
    public int cpf  { get; set; }
    public int idtipofinanciamento { get; set; }

    [Range(5,72, 
        ErrorMessage = "O campo {0} tem que estar entre {1} e {2}.")]
    public int quantidadeparcela { get; set; }
    
    [Range(1, 1000000,
        ErrorMessage = "O campo {0} tem que estar entre {1} e {2}.")]
    public double valorcredito { get; set; }
    
    [Required]
    [CustomDateRange]
    public DateTime dataprimeiraparcela { get; set; }
}