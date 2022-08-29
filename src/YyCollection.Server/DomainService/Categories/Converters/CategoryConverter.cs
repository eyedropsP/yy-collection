using YyCollection.DataStore.Rdb.Core.Queries;
using D = YyCollection.Server.DomainService.Categories.Entities;
using S = YyCollection.DataStore.Rdb.Core.Entities.Tables;

namespace YyCollection.Server.DomainService.Categories.Converters;

/// <summary>
/// カテゴリのレイヤー変換を提供します。
/// </summary>
public static class CategoryConverter
{
    #region Rdb -> Domain Entity
    /// <summary>
    /// Domain Entity に変換します。
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public static D.Category ToDomain(this S.Category category)
        => new()
        {
            Id = category.Id,
            Name = category.Name,
        };
    #endregion


    #region Domain Entity -> Rdb
    /// <summary>
    /// Category の Rdb に変換します。
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public static S.Category ToRdb(this D.Category category)
        => new()
        {
            Id = category.Id,
            Name = category.Name,
        };
    #endregion
}