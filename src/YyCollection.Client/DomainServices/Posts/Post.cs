namespace YyCollection.Client.DomainServices.Posts;

/// <summary>
/// 投稿を表します。
/// </summary>
public sealed class Post
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// 投稿 ID
    /// </summary>
    public Ulid Id { get; init; }

    /// <summary>
    /// 投稿タイトル
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// 概要
    /// </summary>
    public string? Overview { get; init; }

    /// <summary>
    /// 動画 URL
    /// </summary>
    public string MediaUrl { get; init; }

    /// <summary>
    /// シーク開始位置 [s]
    /// </summary>
    public int StartTime { get; init; }

    /// <summary>
    /// シーク終了位置 [s]
    /// </summary>
    public int EndTime { get; init; }

    /// <summary>
    /// 投稿者 ID
    /// </summary>
    public Ulid ContributorId { get; init; }

    /// <summary>
    /// 登録日時
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }
#pragma warning restore CS8618
    #endregion
}