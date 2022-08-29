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
/// <summary>
/// <see cref="IServiceCollection"/> の拡張機能を提供します。
/// </summary>
internal static class IServiceCollectionExtensions
{
    /// <summary>
    /// Open API 関連機能を DI に登録します。
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.TryAddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
        services.AddSwaggerGen();
        return services;
    }


    /// <summary>
    /// <see cref="SwaggerGenOptions"/> を構成します。
    /// </summary>
    private sealed class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        #region プロパティ
        /// <summary>
        /// アプリケーション内の API バージョン情報のプロバイダーを取得します。
        /// </summary>
        private IApiVersionDescriptionProvider Provider { get; }
        #endregion


        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="provider"></param>
        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
            => this.Provider = provider;
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