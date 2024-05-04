using System;
using System.ComponentModel.DataAnnotations;

namespace OpenFinan.Infra.Repository;

public class RepositoryConfiguration
{
    [Required]
    public string SqlConnectionString { get; set; }
}