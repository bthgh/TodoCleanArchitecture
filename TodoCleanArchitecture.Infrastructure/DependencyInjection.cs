
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoCleanArchitecture.Application.Abstractions.Data;
using TodoCleanArchitecture.Application.Abstractions.Repositories;
using TodoCleanArchitecture.Infrastructure.Identity;
using TodoCleanArchitecture.Infrastructure.Persistence.Data;
using TodoCleanArchitecture.Infrastructure.Persistence.Data.Interceptors;
using TodoCleanArchitecture.Infrastructure.Persistence.Repositories.Common;
using TodoCleanArchitecture.Infrastructure.Persistence.Repositories;

namespace TodoCleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Register DbContext
        var connectionString = configuration.GetConnectionString("DbConnectionString");
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString); 
        });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        //Register Identity
        services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        //Register Interceptor Services
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        //Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Register Repositories
        services.AddScoped<ITodoItemRepository,TodoItemRepository>();


        return services;
    }
}