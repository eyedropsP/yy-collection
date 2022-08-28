using YyCollection.Server.DomainService.Categories.Entities;
using YyCollection.Server.DomainService.Tags.Entities;

namespace YyCollection.Server.DomainService.Posts.Entities;

/// <summary>
/// 投稿のリレーションを表します。
/// </summary>
public sealed class PostRelation
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// 投稿を表します。
    /// </summary>
    public Post Post { get; init; }
    
    /// <summary>
    /// カテゴリを表します。
    /// </summary>
    public Category Category { get; init; }
    
    /// <summary>
    /// タグのコレクションを表します。
    /// </summary>
    public IEnumerable<Tag> Tags { get; init; }
#pragma warning restore CS8618
    #endregion
}