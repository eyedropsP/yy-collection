using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YyCollection.DataStore.Rdb.Internals;

namespace YyCollection.DataStore.Rdb.Core.Entities.Tables;

/// <summary>
/// フォローを表します。
/// </summary>
[Table("Follows")]
public sealed class Follow
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// フォロワー ID
    /// </summary>
    [Key]
    [Column("FollowerId", TypeName = "char(26)")]
    public Ulid FollowerId { get; init; }
    
    
    /// <summary>
    /// フォロイー ID
    /// </summary>
    [Key]
    [Column("Followee", TypeName = "char(26)")]
    public Ulid FolloweeId { get; init; }
    
    
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