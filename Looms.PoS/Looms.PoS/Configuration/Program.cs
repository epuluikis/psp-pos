using Looms.PoS.Application;
using Looms.PoS.Application.Options;
using Looms.PoS.Persistance;
using Looms.PoS.Swagger.Filters;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<TwilioOptions>(builder.Configuration.GetSection("Twilio"));

        builder.Services.AddControllers();

        builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
        {
            build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        }));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.OperationFilter<SwaggerRequestTypeOperationFilter>();
            c.OperationFilter<AddBusinessIdHeader>();

            c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        builder.Services.AddApplicationLayer();
        builder.Services.AddPersistanceLayer(builder.Configuration.GetConnectionString("DefaultConnection"));

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            if(!context.Businesses.Any())
            {
                var dataSeeder = new DataSeeder(context);
                dataSeeder.Seed();
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.UseExceptionHandler();
        app.UseCors("corspolicy");

        app.MapControllers();

        app.Run();
    }
}
