using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using YyCollection.DataStore.Rdb.Internals;

namespace YyCollection.DataStore.Rdb;

// ReSharper disable once InconsistentNaming
/// <summary>
/// <see cref="IServiceCollection"/> の拡張機能を提供します。
/// </summary>
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// データベース関連サービスを DI に追加します。
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IServiceCollection AddRdb(this IServiceCollection services, RdbOptions options)
    {
        TypeHandlerProvider.Setup();

        SqlMapper.Settings.CommandTimeout = options.CommandTimeout;

        var factory = new DbConnectionFactory(options);
        services.TryAddSingleton(factory);
        return services;
    }
}