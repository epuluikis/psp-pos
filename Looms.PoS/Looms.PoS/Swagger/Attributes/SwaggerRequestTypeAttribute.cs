namespace Looms.PoS.Swagger.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class SwaggerRequestTypeAttribute : Attribute
{
    public SwaggerRequestTypeAttribute(Type type)
    {
        Type = type;
    }

    public Type Type { get; set; }
}
