using System.ComponentModel.DataAnnotations.Schema;

namespace YyCollection.DataStore.Rdb.Core.Entities.Tables;

/// <summary>
/// ユーザーを表します。
/// </summary>
[Table("Users")]
public sealed class User
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// ユーザー ID
    /// </summary>
    [Column("Id", TypeName = "char(26)")]
    public Ulid Id { get; init; }
    
    
    /// <summary>
    /// 名前
    /// </summary>
    [Column("Name", TypeName = "varchar(255)")]
    public string Name { get; init; }
    
    
    /// <summary>
    /// ユーザーアイコン URL
    /// </summary>
    [Column("Name", TypeName = "varchar(255)")]
    public string IconUrl { get; init; }
    
    
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