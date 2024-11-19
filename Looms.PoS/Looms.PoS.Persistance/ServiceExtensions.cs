
using Looms.PoS.Domain.Interfaces;
using Looms.PoS.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Looms.PoS.Persistance;

public static class ServiceExtensions
{
    public static void AddPersistanceLayer(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IBusinessesRepository, BusinessesRepository>();
        services.AddScoped<IDiscountsRepository, DiscountsRepository>();
        services.AddScoped<IRefundsRepository, RefundsRepository>();
        services.AddScoped<IPaymentsRepository, PaymentsRepository>();
        services.AddScoped<IGiftCardsRepository, GiftCardsRepository>();
        services.AddTransient<IProductsRepository, ProductsRepository>();
        services.AddTransient<IProductVariationRepository, ProductVariationRepository>();
        services.AddTransient<IProductStockRepository, ProductStockRepository>();
    }
}
