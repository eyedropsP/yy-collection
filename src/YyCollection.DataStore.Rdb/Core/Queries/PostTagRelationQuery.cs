using System.Linq.Expressions;
using YyCollection.DataStore.Rdb.Core.Entities.Tables;
using YyCollection.DataStore.Rdb.Internals;

namespace YyCollection.DataStore.Rdb.Core.Queries;

/// <summary>
/// 投稿タグのクエリを提供します。
/// </summary>
public readonly struct PostTagRelationQuery
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
    public PostTagRelationQuery(CoreConnection coreConnection)
        => this.CoreConnection = coreConnection;
    #endregion


    #region 追加
    /// <summary>
    /// 投稿タグを追加します。
    /// </summary>
    /// <param name="postTagRelations"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> InsertMultiAsync(IEnumerable<PostTagRelation> postTagRelations, int? timeout = null, CancellationToken cancellationToken = default)
    {
        var affected = await this.CoreConnection.Primary.InsertMultiAsync(postTagRelations, useAmbientValue: true, timeout, cancellationToken);
        return affected == postTagRelations.Count();
    }
    #endregion


    #region 削除
    /// <summary>
    /// 指定された投稿のタグを削除します。
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<int> DeleteAsync(Ulid postId, int? timeout = null, CancellationToken cancellationToken = default)
    {
        Expression<Func<PostTagRelation, bool>> predicate = x => x.PostId == postId;
        return await this.CoreConnection.Primary.DeleteAsync(predicate, timeout, cancellationToken);
    }
    #endregion
}