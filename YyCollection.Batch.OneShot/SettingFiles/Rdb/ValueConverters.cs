using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace YyCollection.Batch.OneShot.SettingFiles.Rdb;

internal sealed class UlidConverter : ValueConverter<Ulid, string>
{
    #region 定数 
    /// <summary>
    /// 既定のマッピングヒント
    /// </summary>
    private static readonly ConverterMappingHints DefaultHints = new(size: 26);
    #endregion


    #region コンストラクタ
    /// <summary>
    /// インスタンスを生成します。
    /// </summary>
    public UlidConverter()
        : base
        (
            convertToProviderExpression: static x => x.ToString(),
            convertFromProviderExpression: static x => Ulid.Parse(x),
            mappingHints: DefaultHints.With(null)
        )
    { }


    /// <summary>
    /// インスタンスを生成します。
    /// </summary>
    /// <param name="mappingHints"></param>
    public UlidConverter(ConverterMappingHints? mappingHints = null)
        : base
        (
            convertToProviderExpression: static x => x.ToString(),
            convertFromProviderExpression: static x => Ulid.Parse(x),
            mappingHints: DefaultHints.With(mappingHints)
        )
    { }
    #endregion
}