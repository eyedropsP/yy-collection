using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YyCollection.DataStore.Redis.Internals;

/// <summary>
/// <see cref="JsonSerializerOptions"/> のキャッシュを提供します。
/// </summary>
internal static class JsonSerializerOptionsProvider
{
    #region プロパティ
    /// <summary>
    /// 
    /// </summary>
    public static JsonSerializerOptions NoEscapeIgnoreNull { get; } 
    #endregion

    #region コンストラクタ
    /// <summary>
    /// コンストラクタ
    /// </summary>
    static JsonSerializerOptionsProvider()
    {
        NoEscapeIgnoreNull = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
    }
    #endregion
}