using YyCollection.DataStore.Rdb;
using YyCollection.DataStore.Redis;

namespace YyCollection.Server;

/// <summary>
/// アプリケーション設定を表します。
/// </summary>
public sealed class AppSettings
{
#pragma warning disable CS8618
    /// <summary>
    /// データベースの構成を取得します。
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    public RdbOptions Rdb { get; private init; }
    
    /// <summary>
    /// Redis の構成を取得します。
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    public RedisOptions Redis { get; private init; }
#pragma warning restore CS8618
}