namespace YyCollection.Server.DomainService.Categories.Entities;

/// <summary>
/// カテゴリを表します。
/// </summary>
public sealed class Category
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// カテゴリ Id
    /// </summary>
    public Ulid Id { get; init; }

    
    /// <summary>
    /// カテゴリ名
    /// </summary>
    public string Name { get; init; }
#pragma warning restore CS8618
    #endregion
}