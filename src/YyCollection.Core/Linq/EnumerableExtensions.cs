namespace YyCollection.Core.Linq;

/// <summary>
/// <see cref="IEnumerable{T}"/> の拡張機能を提供します。
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// ようそにインデックスを付与します。
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<(T element, int index)> WithIndex<T>(this IEnumerable<T> source)
        => source.Select(static (x, i) => (x, i));
}