using FluentValidation;
using Looms.PoS.Application.Exceptions.Handlers;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Utilities;
using Looms.PoS.Application.Utilities.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Looms.PoS.Application;

public static class ServiceExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddHttpContextAccessor();

        services.AddSingleton<IHttpContentResolver, HttpContentResolver>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceExtensions).Assembly));
        services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);

        services.RegisterExceptionHandling();
    }

    private static void RegisterExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<BadRequestExceptionHandler>();

        services.AddProblemDetails();
    }
}
