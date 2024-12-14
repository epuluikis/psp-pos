
using Looms.PoS.Application;
using Looms.PoS.Persistance;
using Looms.PoS.Swagger.Filters;
using Microsoft.OpenApi.Models;

namespace Looms.PoS;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
        {
            //on my side its localhost:3000, but it might be different when someone else runs it so maybe its fine to leave it as *
            build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        }));

        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.OperationFilter<SwaggerRequestTypeOperationFilter>();

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
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
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });

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
        app.UseCors("corspolicy");

        app.MapControllers();

        app.Run();
    }
}
