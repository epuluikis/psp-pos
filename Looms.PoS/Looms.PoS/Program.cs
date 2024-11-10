
using Looms.PoS.Application;
using Looms.PoS.Domain.Interfaces;
using Looms.PoS.Persistance;
using Looms.PoS.Persistance.Repositories;

namespace Looms.PoS;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddApplicationLayer();
        builder.Services.AddPersistanceLayer(builder.Configuration.GetConnectionString("DefaultConnection"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.UseExceptionHandler();

        app.MapControllers();

        app.Run();
    }
}
