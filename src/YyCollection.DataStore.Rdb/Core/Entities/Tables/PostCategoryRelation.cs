using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YyCollection.DataStore.Rdb.Internals;

namespace YyCollection.DataStore.Rdb.Core.Entities.Tables;

/// <summary>
/// 投稿のカテゴリを表します。
/// </summary>
[Table("PostCategoryRelations")]
public sealed class PostCategoryRelation
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// 投稿 ID
    /// </summary>
    [Key]
    [Column("PostId", TypeName = "char(26)")]
    public Ulid PostId { get; init; }
    
    
    /// <summary>
    /// カテゴリ ID
    /// </summary>
    [Column("CategoryId", TypeName = "char(26)")]
    public Ulid CategoryId { get; init; }
    
    
    /// <summary>
    /// 登録日時
    /// </summary>
    [AmbientValue(KnownConstants.UtcNow)]
    [Column("CreatedAt", TypeName = "timestamp with time zone")]
    public DateTimeOffset CreatedAt { get; init; }
    
    
    /// <summary>
    /// 更新日時
    /// </summary>
    [AmbientValue(KnownConstants.UtcNow)]
    [Column("UpdatedAt", TypeName = "timestamp with time zone")]
    public DateTimeOffset UpdatedAt { get; init; }
#pragma warning restore CS8618
    #endregion
}