namespace YyCollection.DataStore.Redis;

/// <summary>
/// Redis Cache の設定を表します。
/// </summary>
public sealed class RedisOptions
{
#pragma warning disable CS8618
    #region プロパティ
    /// <summary>
    /// 接続文字列を取得します。
    /// </summary>
    public string ConnectionString { get; init; }
    #endregion
#pragma warning restore CS8618
}