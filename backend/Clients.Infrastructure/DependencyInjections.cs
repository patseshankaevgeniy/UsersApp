using Users.Application.Common.Interfaces;
using Users.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}
