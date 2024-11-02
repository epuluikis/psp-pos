using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Looms.PoS.Application;

public static class ServiceExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddSingleton<IHttpContentResolver, HttpContentResolver>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceExtensions).Assembly));
    }
}
