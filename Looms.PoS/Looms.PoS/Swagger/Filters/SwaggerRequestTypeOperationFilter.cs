using Looms.PoS.Swagger.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Looms.PoS.Swagger.Filters;

public class SwaggerRequestTypeOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        SwaggerRequestTypeAttribute? attribute = context.MethodInfo.GetCustomAttribute<SwaggerRequestTypeAttribute>();

        if (attribute == null)
        {
            return;
        }

        operation.RequestBody = new OpenApiRequestBody
        {
            Content =
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(attribute.Type, context.SchemaRepository)
                }
            }
        };
    }
}
