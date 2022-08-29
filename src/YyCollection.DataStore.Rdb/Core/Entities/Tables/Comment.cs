using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YyCollection.DataStore.Rdb.Internals;

namespace YyCollection.DataStore.Rdb.Core.Entities.Tables;

/// <summary>
/// コメントを表します。
/// </summary>
[Table("Comments")]
public sealed class Comment
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// コメント ID
    /// </summary>
    [Key]
    [Column("Id", TypeName = "char(26)")]
    public Ulid Id { get; init; }
    
    
    /// <summary>
    /// 投稿 ID
    /// </summary>
    [Column("PostId", TypeName = "char(26)")]
    public Ulid PostId { get; init; }
    
    
    /// <summary>
    /// ユーザー ID
    /// </summary>
    [Column("UserId", TypeName = "char(26)")]
    public Ulid UserId { get; init; }
    
    
    /// <summary>
    /// コメント
    /// </summary>
    [Column("Comment", TypeName = "text")]
    public string Message { get; init; }
    
    
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