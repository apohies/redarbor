using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Domain.Interfaces;
using Redarbor.Infrastructure.Context;
using Redarbor.Infrastructure.Repositories;

namespace Redarbor.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;

  
        services.AddDbContext<RedarborDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


        services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();

 
        services.AddScoped<IEmployeeReadRepository>(_ =>
            new EmployeeReadRepository(connectionString));

        return services;
    }
}