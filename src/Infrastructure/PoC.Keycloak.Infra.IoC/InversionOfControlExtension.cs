using Microsoft.Extensions.DependencyInjection;

namespace PoC.Keycloak.Infra.IoC;

public static class InversionOfControlExtension
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        //services.AddScoped<IUseCase, UseCase>();
        //services.AddScoped<IRepository, Repository>();
    }
}