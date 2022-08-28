namespace YyCollection.DataStore.Rdb.Internals;

/// <summary>
/// よく利用される定数を表します。
/// </summary>
internal static class KnownConstants
{
    /// <summary>
    /// UTC 時間を返す関数。
    /// </summary>
    public const string UtcNow = "timezone('utc', now())";
}