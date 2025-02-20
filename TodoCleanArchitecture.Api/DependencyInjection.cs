using Asp.Versioning;  
using Microsoft.AspNetCore.Mvc; 
using TodoCleanArchitecture.Api.Filters;
using TodoCleanArchitecture.Api.Middlewares;
using TodoCleanArchitecture.Api.Services;
using TodoCleanArchitecture.Application.Abstractions.Identity;
using TodoCleanArchitecture.Application.Models.ApiResult;
using TodoCleanArchitecture.Infrastructure.Persistence.Data;

namespace TodoCleanArchitecture.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioningService()
            .AddControllersService()
            .AddEndpointsApiExplorer()
            .AddExceptionHandler<ExceptionHandler>() 
            .AddCORSService(configuration);
        
        services.AddScoped<IUser, CurrentUser>();
        services.AddHttpContextAccessor();
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();
             
        return services;
    }
    
    private static IServiceCollection AddApiVersioningService(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1,0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version")
            );
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        }); 
        return services;
    }

    private static IServiceCollection AddControllersService(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(OkResultAttribute));
            options.Filters.Add(typeof(NotFoundResultAttribute));
            options.Filters.Add(typeof(ContentResultFilterAttribute));
            options.Filters.Add(typeof(ModelStateValidationAttribute));
            options.Filters.Add(typeof(BadRequestResultFilterAttribute));
            options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResult<Dictionary<string, List<string>>>),
                StatusCodes.Status400BadRequest));
            options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResult),
                StatusCodes.Status401Unauthorized));
            options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResult),
                StatusCodes.Status403Forbidden));
            options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResult),
                StatusCodes.Status500InternalServerError));

        }).ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
            options.SuppressMapClientErrors = true;
        });

        return services;
    }

    private static IServiceCollection AddCORSService(this IServiceCollection services, IConfiguration configuration)
    {
        var allowUrls = configuration.GetSection("AllowCORSUrls").Get<string[]>()
            ?? throw new ArgumentNullException("CORS Setting is Null!");

        services.AddCors(options =>
        {
            options.AddPolicy("AllowCORSUrls",
                policy =>
                {
                    policy.WithOrigins(allowUrls)
                          .AllowAnyHeader()                     
                          .AllowAnyMethod();                     
                });
        });

        return services;
    }
     
}
