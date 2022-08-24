using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YyCollection.DataStore.Rdb.Core.Entities.Tables;

/// <summary>
/// タグを表します。
/// </summary>
[Table("Tags")]
public sealed class Tag
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// タグ ID
    /// </summary>
    [Key]
    [Column("Id", TypeName = "char(26)")]
    public Ulid Id { get; init; }
    
    
    /// <summary>
    /// タグ名
    /// </summary>
    [Column("Name", TypeName = "varchar(128)")]
    public string Name { get; init; }
    
    
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