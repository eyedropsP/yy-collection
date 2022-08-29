using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace YyCollection.DataStore.Redis;

// ReSharper disable once InconsistentNaming
/// <summary>
/// <see cref="IServiceCollection"/> の拡張機能を提供します。
/// </summary>
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, RedisOptions options)
    {
        services.TryAddSingleton(options);
        services.TryAddSingleton(static provider =>
        {
            var options = provider.GetRequiredService<RedisOptions>();
            return new RedisDbProvider(options);
        });
        return services;
    }
}