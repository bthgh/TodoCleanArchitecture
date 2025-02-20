using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TodoCleanArchitecture.Application.Abstractions.Data;
using TodoCleanArchitecture.Application.Abstractions.Identity;
using TodoCleanArchitecture.Application.Abstractions.Repositories;
using TodoCleanArchitecture.Infrastructure.Identity;
using TodoCleanArchitecture.Infrastructure.Persistence.Data;
using TodoCleanArchitecture.Infrastructure.Persistence.Data.Interceptors;
using TodoCleanArchitecture.Infrastructure.Persistence.Repositories.Common;
using TodoCleanArchitecture.Infrastructure.Persistence.Repositories;

namespace TodoCleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        //Register DbContext
        var connectionString = configuration.GetConnectionString("DbConnectionString");

        //Register Interceptor Services
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        //Register Identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders(); 
        
        services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:Audience"],
                        ValidIssuer = configuration["JWT:Issuer"],
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                    };
                }
            );

        services.AddAuthorization();

        services.AddScoped<ITokenService, TokenService>();
        
        
        
        services.AddSingleton(TimeProvider.System);

        //Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Register Repositories
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();


        return services;
    }
}