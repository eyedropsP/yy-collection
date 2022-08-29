using System.Text.Json;
using CloudStructures.Converters;

namespace YyCollection.DataStore.Redis.Internals;

/// <summary>
/// Redis に格納する値の JSON 形式への変換機能を提供します。
/// </summary>
internal sealed class JsonValueConverter : IValueConverter
{
    #region プロパティ
    /// <summary>
    /// 直列化に利用する <see cref="JsonSerializerOptions"/> を取得します。
    /// </summary>
    private JsonSerializerOptions Options { get; }
    #endregion


    #region コンストラクタ
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="options"></param>
    public JsonValueConverter(JsonSerializerOptions options)
        => this.Options = options;
    #endregion


    #region IValueConverter implementations
    /// <inheritdoc />
    public byte[] Serialize<T>(T value)
        => JsonSerializer.SerializeToUtf8Bytes(value, this.Options);


    /// <inheritdoc />
    public T Deserialize<T>(byte[] value)
        => JsonSerializer.Deserialize<T>(value, this.Options)!;
    #endregion
}