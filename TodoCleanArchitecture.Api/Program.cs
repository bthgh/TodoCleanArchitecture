using Serilog;
using TodoCleanArchitecture.Api;
using TodoCleanArchitecture.Api.Middlewares;
using TodoCleanArchitecture.Application;
using TodoCleanArchitecture.Infrastructure;
using TodoCleanArchitecture.Infrastructure.Identity;
using TodoCleanArchitecture.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(LoggingConfiguration.ConfigureLogger);

// Add services to the container.
builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
} 

app.UseCors("AllowCORSUrls");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
  
app.MapControllers(); 

app.Run();