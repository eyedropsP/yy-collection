using System.ComponentModel.DataAnnotations.Schema;

namespace YyCollection.DataStore.Rdb.Core.Entities.Tables;

/// <summary>
/// マイリストのコンテンツを表します。
/// </summary>
[Table("MyListContents")]
public sealed class MyListContent
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// マイリスト ID
    /// </summary>
    [Column("MyLIstId", TypeName = "char(26)")]
    public Ulid MyListId { get; init; }
    
    
    /// <summary>
    /// 投稿 ID
    /// </summary>
    [Column("PostId", TypeName = "char(26)")]
    public Ulid PostId { get; init; }
    
    
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