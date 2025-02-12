using System.Reflection;
using AutoMapper;
using BrizonForum.Application.Models.Settings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TodoCleanArchitecture.Application.Behaviors;
using TodoCleanArchitecture.Application.MappingProfiles;

namespace TodoCleanArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Register Commands and Queries Validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        //Register MediatR
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); 
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); 
        });


        //Register Automapper 
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new TodoItemMapperProfile()); 
        }).CreateMapper());

        return services;
    }
}