using FluentValidation;
using Looms.PoS.Application.Exceptions.Handlers;
using Looms.PoS.Application.Features.Payment.Handlers;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Mappings.ModelsResolvers;
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
        services.AddAutoMapper(typeof(ServiceExtensions).Assembly);

        services.RegisterExceptionHandling();
        services.RegisterMappers();
        services.RegisterFactories();
    }

    private static void RegisterExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();
    }

    private static void RegisterMappers(this IServiceCollection services)
    {
        services.AddSingleton<IBusinessModelsResolver, BusinessModelsResolver>();
        services.AddSingleton<IUserModelsResolver, UserModelsResolver>();
        services.AddSingleton<IDiscountModelsResolver, DiscountModelsResolver>();
        services.AddSingleton<IRefundModelsResolver, RefundModelsResolver>();
        services.AddSingleton<IPaymentModelsResolver, PaymentModelsResolver>();
        services.AddSingleton<IOrderModelsResolver, OrderModelsResolver>();
        services.AddSingleton<IOrderItemModelsResolver, OrderItemModelsResolver>();
        services.AddSingleton<ITaxModelsResolver, TaxModelsResolver>();
        services.AddSingleton<IGiftCardModelsResolver, GiftCardModelsResolver>();
    }

    private static void RegisterFactories(this IServiceCollection services)
    {
        services.AddScoped<IPaymentHandler, CashPaymentHandler>();
        services.AddScoped<IPaymentHandler, CreditCardPaymentHandler>();
        services.AddScoped<IPaymentHandler, GiftCardPaymentHandler>();

        services.AddScoped<IPaymentHandlerFactory, PaymentHandlerFactory>();
    }
}
