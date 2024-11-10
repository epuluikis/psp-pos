namespace Looms.PoS.Swagger.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class SwaggerRequestTypeAttribute : Attribute
{
    public Type Type { get; set; }

    public SwaggerRequestTypeAttribute(Type type)
    {
        Type = type;
    }
}
