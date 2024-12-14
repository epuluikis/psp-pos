using Looms.PoS.Application.Constants;
using Looms.PoS.Configuration.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Looms.PoS.Configuration;

public class AddBusinessIdHeader : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        if (!context.MethodInfo.GetCustomAttributes(typeof(ExcludeHeaderAttribute)).Any())
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = HeaderConstants.BusinessIdHeader,
                In = ParameterLocation.Header,
                Schema = new()
                {
                    Type = HeaderConstants.TypeString,
                    Format = HeaderConstants.FormatUuid
                },
                Required = true
            });
        }
    }
}