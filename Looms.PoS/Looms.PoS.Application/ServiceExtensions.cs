using FluentValidation;
using Looms.PoS.Application.Exceptions.Handlers;
using Looms.PoS.Application.Factories;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Mappings.ModelsResolvers;
using Looms.PoS.Application.Services;
using Looms.PoS.Application.Services.Notification;
using Looms.PoS.Application.Services.PaymentHandler;
using Looms.PoS.Application.Services.PaymentProvider;
using Looms.PoS.Application.Utilities;
using Looms.PoS.Application.Utilities.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Looms.PoS.Application;

public static class ServiceExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthenticationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(HttpContextBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PermissionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddHttpContextAccessor();

        services.AddSingleton<IHttpContentResolver, HttpContentResolver>();
        services.AddSingleton<ITokenService, TokenService>();
        services.AddSingleton<IDiscountService, DiscountService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddSingleton<IRefundService, RefundService>();
        services.AddSingleton<IPaymentTotalsService, PaymentsTotalsService>();
        services.AddSingleton<IPermissionService, PermissionService>();
        services.AddSingleton<INotificationService, TwilioNotificationService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductVariationService, ProductVariationService>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceExtensions).Assembly));
        services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);
        services.AddAutoMapper(typeof(ServiceExtensions).Assembly);

        services.RegisterExceptionHandling();
        services.RegisterMappers();
        services.RegisterFactories();
    }

    private static void RegisterExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<UnauthorizedExceptionHandler>();
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
        services.AddScoped<IOrderModelsResolver, OrderModelsResolver>();
        services.AddScoped<IOrderItemModelsResolver, OrderItemModelsResolver>();

        services.AddSingleton<IServiceModelsResolver, ServiceModelsResolver>();
        services.AddSingleton<IReservationModelsResolver, ReservationModelsResolver>();
        services.AddSingleton<ITaxModelsResolver, TaxModelsResolver>();
        services.AddSingleton<IGiftCardModelsResolver, GiftCardModelsResolver>();
        services.AddSingleton<IProductModelsResolver, ProductModelsResolver>();
        services.AddSingleton<IProductVariationModelsResolver, ProductVariationModelsResolver>();
        services.AddSingleton<IAuthModelsResolver, AuthModelsResolver>();
        services.AddSingleton<IPaymentProviderModelsResolver, PaymentProviderModelsResolver>();
        services.AddSingleton<IPaymentTerminalModelsResolver, PaymentTerminalModelsResolver>();
    }

    private static void RegisterFactories(this IServiceCollection services)
    {
        services.AddScoped<IPaymentHandlerService, CashPaymentHandlerService>();
        services.AddScoped<IPaymentHandlerService, CreditCardPaymentHandlerService>();
        services.AddScoped<IPaymentHandlerService, GiftCardPaymentHandlerService>();
        services.AddScoped<IPaymentHandlerServiceFactory, PaymentHandlerServiceFactory>();

        services.AddScoped<IPaymentProviderService, StripePaymentProviderService>();
        services.AddScoped<IPaymentProviderServiceFactory, PaymentProviderServiceFactory>();
    }
}
