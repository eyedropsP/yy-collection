using System.Transactions;
using YyCollection.DataStore.Rdb;
using YyCollection.DataStore.Rdb.Core.Entities.Tables;
using YyCollection.DataStore.Rdb.Core.Queries;
using YyCollection.Server.DomainService.Posts.Converters;
using YyCollection.Server.DomainService.Posts.Entities;

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


    /// <summary>
    /// 投稿します。
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> PostAsync(PostRelation postRelation, int? timeout = null, CancellationToken cancellationToken = default)
    {
        using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await using (var conn = this.DbConnectionFactory.CreateCoreConnection())
            {
                var postId = postRelation.Post.Id;

                //--- 投稿
                var post = postRelation.Post.ToRdb();
                var success1 = await new PostQuery(conn).InsertAsync(post, timeout, cancellationToken);
                if (!success1)
                    return false;

                //--- カテゴリ
                var categoryRelation = new PostCategoryRelation
                {
                    PostId = postId,
                    CategoryId = postRelation.Category.Id,
                };
                var success2 = await new PostCategoryRelationQuery(conn).InsertAsync(categoryRelation, timeout, cancellationToken);
                if (!success2)
                    return false;

                //--- タグ
                var tagRelations = postRelation.Tags
                    .Select(x => new PostTagRelation
                    {
                        PostId = postId,
                        TagId = x.Id,
                    });
                var success3 = await new PostTagRelationQuery(conn).InsertMultiAsync(tagRelations, timeout, cancellationToken);
                if (!success3)
                    return false;
            }

            tx.Complete();
            return true;
        }
    }

    /// <summary>
    /// 投稿を更新します。
    /// </summary>
    /// <param name="postRelation"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> UpdateAsync(PostRelation postRelation, int? timeout = null, CancellationToken cancellationToken = default)
    {
        using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await using (var conn = this.DbConnectionFactory.CreateCoreConnection())
            {
                var postId = postRelation.Post.Id;

                //--- 投稿
                var post = postRelation.Post.ToRdb();
                var success1 = await new PostQuery(conn).UpdateAsync(post, timeout, cancellationToken);
                if (!success1)
                    return false;

                //--- カテゴリ
                var categoryRelation = new PostCategoryRelation
                {
                    PostId = postId,
                    CategoryId = postRelation.Category.Id,
                };
                var success2 = await new PostCategoryRelationQuery(conn).UpdateAsync(categoryRelation, timeout, cancellationToken);
                if (!success2)
                    return false;

                //--- タグ
                //--- 全削除
                var tagRelationQuery = new PostTagRelationQuery(conn);
                await tagRelationQuery.DeleteAsync(postId, timeout, cancellationToken);

                var tagRelations = postRelation.Tags
                    .Select(x => new PostTagRelation
                    {
                        PostId = postId,
                        TagId = x.Id,
                    });
                var success3 = await new PostTagRelationQuery(conn).InsertMultiAsync(tagRelations, timeout, cancellationToken);
                if (!success3)
                    return false;
            }

            tx.Complete();
            return true;
        }
    }

    /// <summary>
    /// 投稿を削除します。
    /// </summary>
    /// <param name="id"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> DeleteAsync(Ulid id, int? timeout = null, CancellationToken cancellationToken = default)
    {
        using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await using (var conn = this.DbConnectionFactory.CreateCoreConnection())
            {
                //--- 投稿
                var success1 = await new PostQuery(conn).DeleteAsync(id, timeout, cancellationToken);
                if (!success1)
                    return false;

                //--- カテゴリ
                var success2 = await new PostCategoryRelationQuery(conn).DeleteAsync(id, timeout, cancellationToken);
                if (!success2)
                    return false;

                //--- タグ
                await new PostTagRelationQuery(conn).DeleteAsync(id, timeout, cancellationToken);
            }

            tx.Complete();
            return true;
        }
    }
}