using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YyCollection.DataStore.Rdb.Core.Entities.Tables;

/// <summary>
/// 切り抜き投稿を表します。
/// </summary>
[Table("Posts")]
public sealed class Post
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// 投稿 ID
    /// </summary>
    [Key]
    [Column("Id", TypeName = "char(26)")]
    public Ulid Id { get; init; }


    /// <summary>
    /// タイトル
    /// </summary>
    [Column("Title", TypeName = "varchar(255)")]
    public string Title { get; init; }


    /// <summary>
    /// 投稿者 ID
    /// </summary>
    [Column("UserId", TypeName = "char(26)")]
    public Ulid UserId { get; init; }


    /// <summary>
    /// 動画 URL
    /// </summary>
    [Column("MediaUrl", TypeName = "varchar(255)")]
    public string MediaUrl { get; init; }


    /// <summary>
    /// シーク開始位置 [s]
    /// </summary>
    [Column("StartTime")]
    public int StartTime { get; init; }


    /// <summary>
    /// シーク終了位置 [s]
    /// </summary>
    [Column("EndTime")]
    public int EndTime { get; init; }

    
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