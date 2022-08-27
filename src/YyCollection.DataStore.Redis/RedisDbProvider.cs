using CloudStructures;
using YyCollection.DataStore.Redis.Internals;

namespace YyCollection.DataStore.Redis;

/// <summary>
/// Redis データベースを提供します。
/// </summary>
public sealed class RedisDbProvider
{
    #region プロパティ
    #endregion

    #region コンストラクタ
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="options"></param>
    internal RedisDbProvider(RedisOptions options)
    {
        var config = new RedisConfig("Default", options.ConnectionString);
        var converter = new JsonValueConverter(JsonSerializerOptionsProvider.NoEscapeIgnoreNull);
        var connection = new RedisConnection(config, converter);
    }
    #endregion
}