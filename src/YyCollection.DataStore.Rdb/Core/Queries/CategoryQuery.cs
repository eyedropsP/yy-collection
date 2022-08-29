using System.Linq.Expressions;
using YyCollection.DataStore.Rdb.Core.Entities.Tables;
using YyCollection.DataStore.Rdb.Internals;

namespace YyCollection.DataStore.Rdb.Core.Queries;

/// <summary>
/// カテゴリのクエリを提供します。
/// </summary>
public readonly struct CategoryQuery
{
    #region プロパティ
    /// <summary>
    /// データベースへの接続を取得します。
    /// </summary>
    private CoreConnection CoreConnection { get; }
    #endregion


    #region コンストラクタ
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="coreConnection"></param>
    public CategoryQuery(CoreConnection coreConnection)
        => this.CoreConnection = coreConnection;
    #endregion


    #region 取得
    /// <summary>
    /// 1件取得します。
    /// </summary>
    /// <param name="id"></param>
    /// <param name="members"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<Category?> GetAsync(Ulid id, Expression<Func<Category, object>>? members = null, int? timeout = null, CancellationToken cancellationToken = default)
    {
        Expression<Func<Category, bool>> predicate = x => x.Id == id;
        return await this.CoreConnection.Secondary.SelectFirstOrDefaultAsync(members, predicate, timeout, cancellationToken);
    }

    /// <summary>
    /// 指定された件数取得します。
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="members"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public IAsyncEnumerable<Category> EnumerateAsync(int limit, int offset, Expression<Func<Category, object>>? members = null, int? timeout = null, CancellationToken cancellationToken = default)
        => this.CoreConnection.Secondary.SelectAsync(members, predicate: null, limit, offset, timeout, cancellationToken);
    #endregion
}