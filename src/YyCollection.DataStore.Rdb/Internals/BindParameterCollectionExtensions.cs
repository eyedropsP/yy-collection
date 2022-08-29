using QLimitive;

namespace YyCollection.DataStore.Rdb.Internals;

/// <summary>
/// <see cref="BindParameterCollection"/> の拡張機能を提供します。
/// </summary>
internal static class BindParameterCollectionExtensions
{
    /// <summary>
    /// <see cref="Enum"/> を <see cref="string"/> に変換します。
    /// </summary>
    /// <param name="source"></param>
    public static void ConvertEnumValueToString(this BindParameterCollection source)
    {
        foreach (var x in source)
        {
            if (x.Value is Enum value)
                source[x.Key] = value.ToString();
        }
    }
}