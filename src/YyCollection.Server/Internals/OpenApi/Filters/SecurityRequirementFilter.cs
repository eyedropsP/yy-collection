using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace YyCollection.Server.Internals.OpenApi.Filters;

internal sealed class SecurityRequirementFilter : IOperationFilter
{
    #region プロパティ
    /// <summary>
    /// セキュリティ定義名を取得します。
    /// </summary>
    private string SecurityDefinitionName { get; }
    #endregion


    #region コンストラクタ
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="name"></param>
    public SecurityRequirementFilter(string name)
        => this.SecurityDefinitionName = name;
    #endregion


    #region IOperationFilter implementations
    /// <inheritdoc/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        //--- 匿名アクセスを許可しているかどうか
        var allowAnonymous = context.MethodInfo.CustomAttributes.Any(static x => x.AttributeType == typeof(AllowAnonymousAttribute));
        if (allowAnonymous)
            return;

        //--- 認証を要求
        operation.Security.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = this.SecurityDefinitionName,
                        Type = ReferenceType.SecurityScheme,
                    }
                },
                Array.Empty<string>()
            },
        });
    }
    #endregion
}