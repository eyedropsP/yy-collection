using System.Linq.Expressions;
using YyCollection.DataStore.Rdb.Core.Entities.Tables;
using YyCollection.DataStore.Rdb.Internals;

namespace YyCollection.DataStore.Rdb.Core.Queries;

/// <summary>
/// 投稿のクエリを提供します。
/// </summary>
public readonly struct PostQuery
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
    public PostQuery(CoreConnection coreConnection)
        => this.CoreConnection = coreConnection;
    #endregion


    #region 取得
    #endregion


    #region 追加
    /// <summary>
    /// 投稿を追加します。
    /// </summary>
    /// <param name="post"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> InsertAsync(Post post, int? timeout = null, CancellationToken cancellationToken = default)
    {
        var affected = await this.CoreConnection.Primary.InsertAsync(data: post, useAmbientValue: true, timeout, cancellationToken);
        return affected == 1;
    }
    #endregion


    #region 更新
    /// <summary>
    /// 投稿を更新します。
    /// </summary>
    /// <param name="post"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> UpdateAsync(Post post, int? timeout = null, CancellationToken cancellationToken = default)
    {
        var affected = await this.CoreConnection.Primary.UpdateAsync(
            data: post,
            members: static x => new
            {
                x.Overview,
                x.Title,
                x.StartTime,
                x.EndTime, 
                x.UpdatedAt,
            },
            predicate: x => x.Id == post.Id,
            useAmbientValue: true,
            timeout: timeout,
            cancellationToken: cancellationToken
        );
        return affected == 1;
    }
    #endregion


    #region 削除
    /// <summary>
    /// 投稿を削除します。
    /// </summary>
    /// <param name="id"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> DeleteAsync(Ulid id, int? timeout = null, CancellationToken cancellationToken = default)
    {
        Expression<Func<Post, bool>> predicate = x => x.Id == id;
        var affected = await this.CoreConnection.Primary.DeleteAsync(predicate, timeout, cancellationToken);
        return affected == 1;
    }
    #endregion
}