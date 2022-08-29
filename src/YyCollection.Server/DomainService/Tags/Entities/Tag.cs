namespace YyCollection.Server.DomainService.Tags.Entities;

/// <summary>
/// タグを表します。
/// </summary>
public sealed class Tag
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// タグ ID
    /// </summary>
    public Ulid Id { get; init; }
    
    /// <summary>
    /// タグ名
    /// </summary>
    public string Name { get; init; }
#pragma warning restore CS8618
    #endregion
}