using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using YyCollection.Server.Internals.OpenApi.Filters;

namespace YyCollection.Server.Internals.OpenApi;

// ReSharper disable once InconsistentNaming
internal static class IServiceCollectionExtensions
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.TryAddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
        services.AddSwaggerGen();
        return services;
    }


    private sealed class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        #region プロパティ
        /// <summary>
        /// アプリケーション内の API バージョン情報のプロバイダーを取得します。
        /// </summary>
        private IApiVersionDescriptionProvider Provider { get; }
        #endregion


        #region IConfigureOptions implementations
        public void Configure(SwaggerGenOptions options)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            foreach (var x in this.Provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(x.GroupName, new OpenApiInfo
                {
                    Title = assemblyName,
                    Version = x.ApiVersion.ToString(),
                });
            }
            
            //--- オプション
            options.SupportNonNullableReferenceTypes();
            
            //--- スキーマ ID を完全な型名にする
            options.CustomSchemaIds(static x => x.ToString());

            var xmlDocumentPath = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");
            options.IncludeXmlComments(xmlDocumentPath);
            
            //--- 認証
            const string securityName = "JWT Bearer Authentication";
            options.AddSecurityDefinition(securityName, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            
            //--- フィルター
            options.OperationFilter<SecurityRequirementFilter>(securityName);
            options.SchemaFilter<UlidSchemaFilter>();
        }
        #endregion
    }
}