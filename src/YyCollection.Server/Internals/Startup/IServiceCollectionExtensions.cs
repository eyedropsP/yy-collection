#nullable enable
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using YyCollection.DataStore.Rdb;
using YyCollection.DataStore.Redis;

namespace YyCollection.Server.Internals.Startup;

/// <summary>
/// <see cref="IServiceCollection"/> の拡張機能を提供します。
/// </summary>
// ReSharper disable once InconsistentNaming
internal static class IServiceCollectionExtensions
{
    /// <summary>
    /// アプリケーションの構成情報を DI に登録します。
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static AppSettings ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var match = Regex.Match(configuration.GetValue<string>("RDB_URL_PRIMARY")!, @"postgres://(.*):(.*)@(.*):(.*)/(.*)");
        var rdbConnectionString = $"Server={match.Groups[3]};Port={match.Groups[4]};User Id={match.Groups[1]};Password={match.Groups[2]};Database={match.Groups[5]};sslmode=Prefer;Trust Server Certificate=true";
        var commandTimeout = configuration.GetValue<int>("RDB_COMMAND_TIMEOUT");
        var masterCacheExpiry = configuration.GetValue<TimeSpan>("RDB_MASTER_CACHE_EXPIRY");
        var redisConnectionString = configuration.GetValue<string>("REDIS_TLS_URL");
        var appSettings = new AppSettings
        {
            Rdb = new RdbOptions
            {
                Core = new RdbOptions.ConnectionSetting
                {
                    Primary = rdbConnectionString,
                    Secondary = rdbConnectionString,
                },
                CommandTimeout = commandTimeout,
                MasterCacheExpiry = masterCacheExpiry,
            },
            Redis = new RedisOptions
            {
                ConnectionString = redisConnectionString,
            },
        };
        
        services.TryAddSingleton(appSettings);
        return appSettings;
    }


    /// <summary>
    /// アプリケーション固有のサービスを DI に登録します。
    /// </summary>
    /// <param name="services"></param>
    /// <param name="appSettings"></param>
    /// <returns></returns>
    public static IServiceCollection AddDomainServices(this IServiceCollection services, AppSettings appSettings)
    {
        //--- Project Libraries
        services.AddRdb(appSettings.Rdb);
        services.AddRedis(appSettings.Redis);
        
        //--- Domain Services
        services.TryAddSingleton<DomainService.Posts.PostService>();

        return services;
    }


    /// <summary>
    /// 高速化関連機能を DI に登録します。
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddPerformance(this IServiceCollection services)
    {
        services.AddResponseCompression();
        return services;
    }
    
        
    /// <summary>
    /// 認証 / 認可機能を DI に登録します。
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        return services;
    }


    /// <summary>
    /// ルーティング機能を DI に登録します。
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRequestRouting(this IServiceCollection services)
    {
        services.AddRouting(static o =>
        {
            o.LowercaseUrls = true;
        });
        return services;
    }


    /// <summary>
    /// ASP.NET Core MVC 関連機能を DI に登録します。
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IMvcBuilder AddAspNetCoreMvc(this IServiceCollection services)
    {
        var mvcBuilder = services.AddControllers();
        
        services.AddApiVersioning(static o =>
        {
            o.ReportApiVersions = true;
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = ApiVersion.Default;
        });

        services.AddVersionedApiExplorer(static o =>
        {
            o.GroupNameFormat = "'v'VVV"; // 'v'major[.minor][-status]
            o.SubstituteApiVersionInUrl = true; // URL 内にある placeholder を置換
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = ApiVersion.Default;
        });
        
        return mvcBuilder;
    }
}