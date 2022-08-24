using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YyCollection.DataStore.Rdb.Core.Entities.Tables;

/// <summary>
/// いいねを表します。
/// </summary>
[Table("Likes")]
public sealed class Like
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
    /// ユーザー ID
    /// </summary>
    [Key]
    [Column("UserId", TypeName = "char(26)")]
    public Ulid UserId { get; init; }
    
 
    /// <summary>
    /// 登録日時
    /// </summary>
    [Column("CreatedAt", TypeName = "timestamp with time zone")]
    public DateTimeOffset CreatedAt { get; init; }
    
    
    /// <summary>
    /// 更新日時
    /// </summary>
    [Column("UpdatedAt", TypeName = "timestamp with time zone")]
    public DateTimeOffset UpdatedAt { get; init; }
#pragma warning restore CS8618
    #endregion
}