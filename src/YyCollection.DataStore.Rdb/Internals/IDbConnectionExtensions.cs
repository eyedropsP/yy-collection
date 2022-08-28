using System.Data;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Cysharp.Text;
using Dapper;
using QLimitive;
using ValueTaskSupplement;

namespace YyCollection.DataStore.Rdb.Internals;

/// <summary>
/// Provides <see cref="IDbConnection"/> extension methods.
/// </summary>

// ReSharper disable once InconsistentNaming
internal static class IDbConnectionExtensions
{
    #region プロパティ
    /// <summary>
    /// データベースの方言を取得します。
    /// </summary>
    private static DbDialect Dialect => DbDialect.PostgreSql;
    #endregion


    #region CancellationToken Support
    /// <summary>
    /// Execute a command asynchronously using Task.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static ValueTask<int> ExecuteAsync(this IDbConnection connection, string sql, object? param, int? timeout, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(sql, param, null, timeout, null, CommandFlags.Buffered, cancellationToken);
        return connection.ExecuteAsync(command).AsValueTask();
    }

    /// <summary>
    /// Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static ValueTask<T?> QueryFirstOrDefaultAsync<T>(this IDbConnection connection, string sql, object? param, int? timeout, CancellationToken cancellationToken)
    {
        const CommandFlags flags = CommandFlags.None;
        var command = new CommandDefinition(sql, param, null, timeout, null, flags, cancellationToken);
        return connection.QueryFirstOrDefaultAsync<T?>(command).AsValueTask();
    }

    /// <summary>
    /// Execute a query asynchronously using IAsyncEnumerable.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async IAsyncEnumerable<T> StreamAsync<T>(this IDbConnection connection, string sql, object? param, int? timeout, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const CommandFlags flags = CommandFlags.None;
        var command = new CommandDefinition(sql, param, null, timeout, null, flags, cancellationToken);
        var result = await connection.QueryAsync<T>(command).ConfigureAwait(false);
        foreach (var x in result)
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;
            yield return x;
        }
    }
    #endregion


    #region CRUD shorthand
    /// <summary>
    /// Gets first record that match the specified condition from the specified table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="members"></param>
    /// <param name="predicate"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ValueTask<T?> SelectFirstOrDefaultAsync<T>(this IDbConnection connection, Expression<Func<T, object>>? members, Expression<Func<T, bool>> predicate, int? timeout, CancellationToken cancellationToken)
    {
        var (sql, param) = CreateSelectFirstQuery(members, predicate);
        return connection.QueryFirstOrDefaultAsync<T>(sql, param, timeout, cancellationToken);
    }

    /// <summary>
    /// Gets records that match the specified condition from the specified table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="members"></param>
    /// <param name="predicate"></param>
    /// <param name="offset"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="limit"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IAsyncEnumerable<T> SelectAsync<T>(this IDbConnection connection, Expression<Func<T, object>>? members, Expression<Func<T, bool>>? predicate, int limit, int offset, int? timeout, CancellationToken cancellationToken)
    {
        Query query;
        using (var builder = new QueryBuilder<T>(Dialect))
        {
            builder.Select(members);
            if (predicate is not null)
                builder.Where(predicate);

            builder.AsIs(static (ref Utf16ValueStringBuilder stringBuilder, ref BindParameterCollection? bindParameters) =>
            {
                stringBuilder.AppendLine();
                stringBuilder.Append("limit ");
                stringBuilder.Append(Dialect.BindParameterPrefix);
                stringBuilder.Append(nameof(limit));
                stringBuilder.AppendLine();
                stringBuilder.Append("offset ");
                stringBuilder.Append(Dialect.BindParameterPrefix);
                stringBuilder.Append(nameof(offset));
            });

            query = builder.Build();
        }

        var (sql, param) = query;
        param?.ConvertEnumValueToString();
        param ??= new();
        param.Add(nameof(limit), limit);
        param.Add(nameof(offset), offset);
        return connection.StreamAsync<T>(sql, param, timeout, cancellationToken);
    }

    /// <summary>
    /// Inserts the specified data into the table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="data"></param>
    /// <param name="useAmbientValue"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ValueTask<int> InsertAsync<T>(this IDbConnection connection, T data, bool useAmbientValue, int? timeout, CancellationToken cancellationToken)
    {
        var (sql, param) = QueryBuilder.Insert<T>(Dialect, useAmbientValue);
        param?.Overwrite(data);
        param?.ConvertEnumValueToString();
        return connection.ExecuteAsync(sql, param, timeout, cancellationToken);
    }

    /// <summary>
    /// Inserts the specified data into the table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="data"></param>
    /// <param name="useAmbientValue"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ValueTask<int> InsertMultiAsync<T>(this IDbConnection connection, IEnumerable<T> data, bool useAmbientValue, int? timeout, CancellationToken cancellationToken)
    {
        var (sql, param) = QueryBuilder.Insert<T>(Dialect, useAmbientValue);
        if (param is null)
        {
            return connection.ExecuteAsync(sql, param, timeout, cancellationToken);
        }

        var @params = data.Select(x =>
        {
            var clone = param.Clone();
            clone.Overwrite(x);
            clone.ConvertEnumValueToString();
            return clone;
        });
        return connection.ExecuteAsync(sql, @params, timeout, cancellationToken);
    }

    /// <summary>
    /// Updates records that match the specified conditions with the specified data.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="data"></param>
    /// <param name="members"></param>
    /// <param name="predicate"></param>
    /// <param name="useAmbientValue"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ValueTask<int> UpdateAsync<T>(this IDbConnection connection, T data, Expression<Func<T, object>> members, Expression<Func<T, bool>> predicate, bool useAmbientValue, int? timeout, CancellationToken cancellationToken)
    {
        Query query;
        using (var builder = new QueryBuilder<T>(Dialect))
        {
            builder.Update(members, useAmbientValue);
            builder.Where(predicate);
            query = builder.Build();
        }

        query.Parameters?.Overwrite(data);
        query.Parameters?.ConvertEnumValueToString();
        return connection.ExecuteAsync(query.Text, query.Parameters, timeout, cancellationToken);
    }

    /// <summary>
    /// Deletes records that match the specified conditions from the specified table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="predicate"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ValueTask<int> DeleteAsync<T>(this IDbConnection connection, Expression<Func<T, bool>> predicate, int? timeout, CancellationToken cancellationToken)
    {
        Query query;
        using (var builder = new QueryBuilder<T>(Dialect))
        {
            builder.Delete();
            builder.Where(predicate);
            query = builder.Build();
        }

        query.Parameters?.ConvertEnumValueToString();
        return connection.ExecuteAsync(query.Text, query.Parameters, timeout, cancellationToken);
    }
    #endregion


    #region Helpers
    /// <summary>
    /// Create query for SelectFirstAsync / SelectFirstOrDefaultAsync.
    /// </summary>
    /// <param name="members"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Query CreateSelectFirstQuery<T>(Expression<Func<T, object>>? members, Expression<Func<T, bool>>? predicate)
    {
        using (var builder = new QueryBuilder<T>(Dialect))
        {
            builder.Select(members);
            if (predicate is not null)
                builder.Where(predicate);

            builder.AsIs(static (ref Utf16ValueStringBuilder stringBuilder, ref BindParameterCollection? bindParameters) =>
            {
                const int limit = 1;

                stringBuilder.AppendLine();
                stringBuilder.Append("limit ");
                stringBuilder.Append(Dialect.BindParameterPrefix);
                stringBuilder.Append(nameof(limit));

                bindParameters ??= new();
                bindParameters.Add(nameof(limit), limit);
            });

            var query = builder.Build();
            query.Parameters?.ConvertEnumValueToString();
            return query;
        }
    }
    #endregion
}