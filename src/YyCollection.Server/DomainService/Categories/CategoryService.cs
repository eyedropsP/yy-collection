using YyCollection.DataStore.Rdb;
using YyCollection.DataStore.Rdb.Core.Queries;
using YyCollection.Server.DomainService.Categories.Converters;
using YyCollection.Server.DomainService.Categories.Entities;

namespace YyCollection.Server.DomainService.Categories;

/// <summary>
/// カテゴリの関連処理を提供します。
/// </summary>
public sealed class CategoryService
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
    public CategoryService(DbConnectionFactory dbConnectionFactory)
        => this.DbConnectionFactory = dbConnectionFactory;
    #endregion
    
    /// <summary>
    /// 指定された ID のカテゴリを取得します。
    /// </summary>
    /// <param name="id"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<Category?> GetAsync(Ulid id, int? timeout = null, CancellationToken cancellationToken = default)
    {
        await using (var conn = this.DbConnectionFactory.CreateCoreConnection())
        {
            var category = await new CategoryQuery(conn).GetAsync(id, timeout: timeout, cancellationToken: cancellationToken);
            return category?.ToDomain();
        }
    }

    /// <summary>
    /// 指定された件数カテゴリを取得します。
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<Category[]> GetAsync(int limit, int offset, int? timeout = null, CancellationToken cancellationToken = default)
    {
        await using (var conn = this.DbConnectionFactory.CreateCoreConnection())
        {
            return await new CategoryQuery(conn)
                .EnumerateAsync(limit, offset, timeout: timeout, cancellationToken: cancellationToken)
                .Select(static x => x.ToDomain())
                .ToArrayAsync(cancellationToken);
        }
    }
}