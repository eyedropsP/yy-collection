#nullable enable

using Microsoft.Extensions.DependencyInjection.Extensions;
using YyCollection.DataStore.Rdb;

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
        var appSettings = configuration.Get<AppSettings>(static o => o.BindNonPublicProperties = true);
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

        return services;
    }
}