using YyCollection.DataStore.Rdb;
using YyCollection.DataStore.Rdb.Core.Queries;
using YyCollection.Server.DomainService.Tags.Converters;
using YyCollection.Server.DomainService.Tags.Entities;

namespace YyCollection.Server.DomainService.Tags;

/// <summary>
/// タグの関連処理を提供します。
/// </summary>
public sealed class TagService
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
    public TagService(DbConnectionFactory dbConnectionFactory)
        => this.DbConnectionFactory = dbConnectionFactory;
    #endregion

    /// <summary>
    /// 指定された ID のタグのコレクションを取得します。
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<Tag[]> GetAsync(IEnumerable<Ulid> ids, int? timeout = null, CancellationToken cancellationToken = default)
    {
        await using (var conn = this.DbConnectionFactory.CreateCoreConnection())
        {
            return await new TagQuery(conn)
                .EnumerateAsync(ids, timeout: timeout, cancellationToken: cancellationToken)
                .Select(static x => x.ToDomain())
                .ToArrayAsync(cancellationToken);
        }
    }

    /// <summary>
    /// 指定された件数のタグを取得します。
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<Tag[]> GetAsync(int limit, int offset, int? timeout = null, CancellationToken cancellationToken = default)
    {
        await using (var conn = this.DbConnectionFactory.CreateCoreConnection())
        {
            return await new TagQuery(conn)
                .EnumerateAsync(limit, offset, timeout: timeout, cancellationToken: cancellationToken)
                .Select(static x => x.ToDomain())
                .ToArrayAsync(cancellationToken);
        }
    }
}