using Users.Application.Common.Interfaces;
using Users.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Persistence;

public static class DependencyInjections
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services
         .AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>())
         .AddDbContext<ApplicationDbContext>(options =>
         {
             var connectionString = configuration.GetConnectionString("DefaultConnection");
             options.UseSqlServer(connectionString);
             options.EnableSensitiveDataLogging();
         });

        return services;
    }
}
