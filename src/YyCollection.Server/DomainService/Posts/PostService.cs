using YyCollection.DataStore.Rdb;

namespace YyCollection.Server.DomainService.Posts;

/// <summary>
/// 投稿のサービスを表します。
/// </summary>
public sealed class PostService
{
    #region プロパティ
    /// <summary>
    /// データベース接続の生成機能を取得します。
    /// </summary>
    private DbConnectionFactory DbConnectionFactory { get; }
    #endregion


    #region コンストラクタ
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="dbConnectionFactory"></param>
    public PostService(DbConnectionFactory dbConnectionFactory)
        => this.DbConnectionFactory = dbConnectionFactory;
    #endregion


    
}