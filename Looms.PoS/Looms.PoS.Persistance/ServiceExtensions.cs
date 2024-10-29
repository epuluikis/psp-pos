
using Looms.PoS.Domain.Interfaces;
using Looms.PoS.Persistance.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Looms.PoS.Persistance;

public static class ServiceExtensions
{
    public static void AddPersistanceLayer(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();

        services.AddScoped<IBusinessesRepository, BusinessesRepository>();
    }
}
