using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace YyCollection.Server.Internals.OpenApi;

// ReSharper disable once InconsistentNaming
internal static class IApplicationBuilderExtensions
{
    public static void UseOpenApi(this IApplicationBuilder builder)
    {
        var uiOptions = new SwaggerUIOptions();

        var provider = builder.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var x in provider.ApiVersionDescriptions)
        {
            var url = $"/swagger/{x.GroupName}/swagger.json";
            uiOptions.SwaggerEndpoint(url, x.GroupName);
        }
        
        uiOptions.DocExpansion(DocExpansion.List);

        builder.UseSwagger();
        builder.UseSwaggerUI(uiOptions);
    }
}