using System;
using OpenFinan.Application.Services;
using OpenFinan.Domain.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddProjectModelCoreApplication(this IServiceCollection services)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<ITipoFinanciamentoService, TipoFinanciamentoService>();
        services.AddScoped<IFinanciamentoService, FinanciamentoService>();
        
        return services;
    }
}