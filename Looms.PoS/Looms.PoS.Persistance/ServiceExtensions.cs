﻿
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

        services.AddTransient<IBusinessesRepository, BusinessesRepository>();
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddTransient<IPaymentsRepository, PaymentsRepository>();
        services.AddTransient<IGiftCardsRepository, GiftCardsRepository>();
    }
}
