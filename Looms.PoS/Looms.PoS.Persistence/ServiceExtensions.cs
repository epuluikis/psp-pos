using Looms.PoS.Domain.Interfaces;
using Looms.PoS.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Looms.PoS.Persistence;

public static class ServiceExtensions
{
    public static void AddPersistenceLayer(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseNpgsql(connectionString));

        services.AddScoped<IBusinessesRepository, BusinessesRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IDiscountsRepository, DiscountsRepository>();
        services.AddScoped<IRefundsRepository, RefundsRepository>();
        services.AddScoped<IPaymentsRepository, PaymentsRepository>();
        services.AddScoped<IGiftCardsRepository, GiftCardsRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
        services.AddScoped<IServicesRepository, ServicesRepository>();
        services.AddScoped<IReservationsRepository, ReservationsRepository>();
        services.AddScoped<ITaxesRepository, TaxesRepository>();
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<IProductVariationRepository, ProductVariationRepository>();
        services.AddScoped<IPaymentProvidersRepository, PaymentProvidersRepository>();
        services.AddScoped<IPaymentTerminalsRepository, PaymentTerminalsRepository>();
    }
}
