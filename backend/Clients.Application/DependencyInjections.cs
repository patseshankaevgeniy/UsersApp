using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Auth;
using Users.Application.Clients.Models;
using Users.Application.Users.Models;
using Users.Application.Users.Services;

namespace Users.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IAuthService, AuthService>();

        // Mappers
        services.AddAutoMapper(typeof(UserAutoMapper));

        // Validators
        services.AddScoped<IValidator<CreatedUserModel>, UserModelValidator>();

        return services;
    }
}
