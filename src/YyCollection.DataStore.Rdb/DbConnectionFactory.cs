using YyCollection.DataStore.Rdb.Core;

namespace YyCollection.DataStore.Rdb;

/// <summary>
/// データベース接続の生成機能を提供します。
/// </summary>
public sealed class DbConnectionFactory
{
    #region プロパティ
    /// <summary>
    /// データベースの構成情報を取得します。
    /// </summary>
    private RdbOptions Options { get; }
    #endregion


    #region コンストラクタ
    /// <summary>
    /// インスタンスを生成します。
    /// </summary>
    /// <param name="options"></param>
    internal DbConnectionFactory(RdbOptions options)
        => Options = options;
    #endregion


    /// <summary>
    /// メインデータベースへの接続を生成します。
    /// </summary>
    /// <param name="forcePrimary"></param>
    /// <returns></returns>
    public CoreConnection CreateCoreConnection(bool forcePrimary = false)
        => new(this.Options.Core, forcePrimary);
}