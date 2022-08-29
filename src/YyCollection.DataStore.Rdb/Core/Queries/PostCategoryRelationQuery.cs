using System.Linq.Expressions;
using YyCollection.DataStore.Rdb.Core.Entities.Tables;
using YyCollection.DataStore.Rdb.Internals;

namespace YyCollection.DataStore.Rdb.Core.Queries;

/// <summary>
/// 投稿カテゴリのクエリを提供します。
/// </summary>
public readonly struct PostCategoryRelationQuery
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
    public PostCategoryRelationQuery(CoreConnection coreConnection)
        => this.CoreConnection = coreConnection;
    #endregion


    #region 追加
    /// <summary>
    /// 投稿カテゴリを追加します。
    /// </summary>
    /// <param name="postCategoryRelation"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> InsertAsync(PostCategoryRelation postCategoryRelation, int? timeout = null, CancellationToken cancellationToken = default)
    {
        var affected = await this.CoreConnection.Primary.InsertAsync(postCategoryRelation, useAmbientValue: true, timeout, cancellationToken);
        return affected == 1;
    }
    #endregion


    #region 更新
    /// <summary>
    /// 投稿カテゴリを更新します。
    /// </summary>
    /// <param name="postCategoryRelation"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> UpdateAsync(PostCategoryRelation postCategoryRelation, int? timeout = null, CancellationToken cancellationToken = default)
    {
        var affected = await this.CoreConnection.Primary
            .UpdateAsync(
                data: postCategoryRelation,
                members: static x => new
                {
                    x.CategoryId,
                    x.UpdatedAt
                },
                predicate: x => x.PostId == postCategoryRelation.PostId,
                useAmbientValue: true,
                timeout: timeout,
                cancellationToken: cancellationToken
            );
        return affected == 1;
    }
    #endregion


    #region 削除
    /// <summary>
    /// 指定された投稿のカテゴリを削除します。
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> DeleteAsync(Ulid postId, int? timeout = null, CancellationToken cancellationToken = default)
    {
        Expression<Func<PostCategoryRelation, bool>> predicate = x => x.PostId == postId;
        var affected = await this.CoreConnection.Primary.DeleteAsync(predicate, timeout, cancellationToken);
        return affected == 1;
    }
    #endregion
}