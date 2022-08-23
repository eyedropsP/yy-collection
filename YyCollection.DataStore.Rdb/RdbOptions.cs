namespace YyCollection.DataStore.Rdb;

/// <summary>
/// データベースの設定を表します。
/// </summary>
public sealed class RdbOptions
{
    #region プロパティ
    /// <summary>
    /// メインデータベースへの接続設定を取得します。
    /// </summary>
    public ConnectionSetting Core { get; init; }
    
    /// <summary>
    /// 既定のタイムアウト時間 [s] を取得します。 
    /// </summary>
    public int? CommandTimeout { get; init; }

    
    /// <summary>
    /// マスタキャッシュの有効期限を取得します。
    /// </summary>
    public TimeSpan MasterCacheExpiry { get; init; }
    #endregion

    
    #region Nested Type
    /// <summary>
    /// 接続設定を取得します。 
    /// </summary>
    public sealed class ConnectionSetting
    {
        /// <summary>
        /// プライマリ DB への接続文字列を取得します。
        /// </summary>
        public string Primary { get; init; }
        
        /// <summary>
        /// セカンダリ DB への接続文字列を取得します。
        /// </summary>
        public string Secondary { get; init; }
    }
    #endregion
}