using D = YyCollection.Server.DomainService.Tags.Entities;
using S = YyCollection.DataStore.Rdb.Core.Entities.Tables;

namespace YyCollection.Server.DomainService.Tags.Converters;

/// <summary>
/// タグのレイヤー変換を提供します。
/// </summary>
public static class TagConverter
{
    #region Rdb -> Domain Entity
    /// <summary>
    /// Domain Entity に変換します。
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static D.Tag ToDomain(this S.Tag tag)
        => new()
        {
            Id = tag.Id,
            Name = tag.Name,
        };
    #endregion
    
    #region Domain Entity -> Rdb
    /// <summary>
    /// Tag の Rdb に変換します。
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static S.Tag ToRdb(this D.Tag tag)
        => new()
        {
            Id = tag.Id,
            Name = tag.Name,
        };
    #endregion
}