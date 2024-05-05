using System;
using System.Data;
using OpenFinan.Domain.Repositories;
using OpenFinan.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;


namespace Microsoft.Extensions.DependencyInjection;

public static class OpenFinanRepositoryServiceCollectionExtensions
{
    public static IServiceCollection AddOpenFinanRepository(this IServiceCollection services, RepositoryConfiguration configuration)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        services.AddScoped<IClienteReadOnlyRepository, ClienteRepository>();
        services.AddScoped<IClienteWriteOnlyRepository, ClienteRepository>();
        services.AddScoped<ITipoFinanciamentoReadOnlyRepository, TipoFinanciamentoRepository>();
        services.AddScoped<ITipoFinanciamentoWriteOnlyRepository, TipoFinanciamentoRepository>();
        services.AddScoped<IFinanciamentoReadOnlyRepository, FinanciamentoRepository>();
        services.AddScoped<IFinanciamentoWriteOnlyRepository, FinanciamentoRepository>();
        services.AddScoped<IParcelaFinanciamentoReadOnlyRepository, ParcelaFinanciamentoRepository>();
        services.AddScoped<IParcelaFinanciamentoWriteOnlyRepository, ParcelaFinanciamentoRepository>();

        services.AddScoped<IDbConnection>(d =>
        {
            return new MySqlConnection(configuration.SqlConnectionString);
        });

        return services;
    }
}