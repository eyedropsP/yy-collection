using System.Linq.Expressions;
using Cysharp.Text;
using QLimitive;
using YyCollection.Core.Linq;
using YyCollection.DataStore.Rdb.Core.Entities.Tables;
using YyCollection.DataStore.Rdb.Internals;

namespace YyCollection.DataStore.Rdb.Core.Queries;

/// <summary>
/// タグのクエリを提供します。
/// </summary>
public readonly struct TagQuery
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
    public TagQuery(CoreConnection coreConnection)
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
    public async ValueTask<Tag?> GetAsync(Ulid id, Expression<Func<Tag, object>>? members = null, int? timeout = null, CancellationToken cancellationToken = default)
    {
        Expression<Func<Tag, bool>> predicate = x => x.Id == id;
        return await this.CoreConnection.Secondary.SelectFirstOrDefaultAsync(members, predicate, timeout, cancellationToken);
    }

    /// <summary>
    /// 指定された ID のタグを取得します。
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="members"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public IAsyncEnumerable<Tag> EnumerateAsync(IEnumerable<Ulid> ids, Expression<Func<Tag, object>>? members = null, int? timeout = null, CancellationToken cancellationToken = default)
    {
        Query query;
        using (var builder = new QueryBuilder<Tag>(this.CoreConnection.Dialect))
        {
            builder.Select(members);
            builder.AsIs(static (ref Utf16ValueStringBuilder stringBuilder, ref BindParameterCollection? bindParameters, (IEnumerable<Ulid> ids, DbDialect dialect) state) =>
                {
                    stringBuilder.AppendLine("where");

                    foreach (var x in state.ids.Chunk(state.dialect.InOperatorMaxCount).WithIndex())
                    {
                        if(x.index > 0)
                            stringBuilder.Append(" or ");

                        var bracket = state.dialect.KeywordBracket;
                        stringBuilder.Append(bracket.Begin);
                        stringBuilder.Append("Id");
                        stringBuilder.Append(bracket.End);
                        stringBuilder.Append(" in ");
                        stringBuilder.Append(state.dialect.BindParameterPrefix);
                        var param = $"ids_{x.index}";
                        stringBuilder.Append(param);

                        bindParameters ??= new BindParameterCollection();
                        bindParameters.Add(param, x.element.Select(static x => x.ToString()));
                    }
                }, (ids, this.CoreConnection.Dialect));
            query = builder.Build();
        }

        var (sql, param) = query;
        param?.ConvertEnumValueToString();
        
        return this.CoreConnection.Secondary.StreamAsync<Tag>(sql, param, timeout, cancellationToken);
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
    public IAsyncEnumerable<Tag> EnumerateAsync(int limit, int offset, Expression<Func<Tag, object>>? members = null, int? timeout = null, CancellationToken cancellationToken = default)
        => this.CoreConnection.Secondary.SelectAsync(members, predicate: null, limit, offset, timeout, cancellationToken);
    #endregion
}