using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace YyCollection.Server.Internals.OpenApi.Filters;

/// <summary>
/// Swagger UI で <see cref="Ulid"/> を使用した場合の型変換を提供します。
/// </summary>
public class UlidSchemaFilter : ISchemaFilter
{
    /// <inheritdoc />
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        const string schemaType = "string";
        if (context.Type.IsAssignableTo(typeof(IEnumerable<Ulid>))) // コレクション
        {
            schema.Items.Type = schemaType;
        }
        else if(context.Type == typeof(Ulid)) // Ulid そのもの
        {
            schema.Type = schemaType;
        }
        throw new NotImplementedException();
    }
}