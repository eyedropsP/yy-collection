using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using YyCollection.Definitions.Enums;

namespace YyCollection.DataStore.Rdb.Core.Entities.Tables;

/// <summary>
/// マイリストを表します。
/// </summary>
[Table("MyLists")]
public sealed class MyList
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// マイリスト ID
    /// </summary>
    [Column("Id", TypeName = "char(26)")]
    public Ulid Id { get; init; }


    /// <summary>
    /// マイリスト名
    /// </summary>
    [Column("Title", TypeName = "varchar(255)")]
    public string Title { get; init; }


    /// <summary>
    /// ユーザー ID
    /// </summary>
    [Column("UserId", TypeName = "char(26)")]
    public Ulid UserId { get; init; }


    /// <summary>
    /// 公開設定
    /// </summary>
    [Column("PrivacyStatus")]
    [DefaultValue(nameof(PrivacyStatus.Private))]
    public PrivacyStatus PrivacyStatus { get; init; }


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