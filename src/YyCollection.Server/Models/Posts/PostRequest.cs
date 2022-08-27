using System.Text.Json.Serialization;
using YyCollection.Server.DomainService.Posts.Entities;

namespace YyCollection.Server.Models.Posts;

/// <summary>
/// /Posts/ の API リクエストを表します。
/// </summary>
public sealed class PostRequest
{
    #region プロパティ
#pragma warning disable CS8618
    /// <summary>
    /// 投稿タイトル
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("title")]
    public string Title { get; init; }
    
    
    /// <summary>
    /// 概要
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("description")]
    public string? Overview { get; init; }


    /// <summary>
    /// 元動画 URL
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("mediaUrl")]
    public string MediaUrl { get; init; }
    
    
    /// <summary>
    /// シーク開始位置 [s]
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("startTime")]
    public int StartTime { get; init; }
    
    
    /// <summary>
    /// シーク終了位置 [s]
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("endTime")]
    public int EndTime { get; init; }
    
    
    /// <summary>
    /// 投稿者 ID
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("contributorId")]
    public Ulid ContributorId { get; init; }
#pragma warning restore CS8618
    #endregion


    #region 変換
    /// <summary>
    /// ドメインモデルに変換します。
    /// </summary>
    /// <returns></returns>
    public Post ToDomain()
        => new()
        {
            Id = Ulid.NewUlid(),
            Title = this.Title,
            Overview = this.Overview,
            MediaUrl = this.MediaUrl,
            StartTime = this.StartTime,
            EndTime = this.EndTime,
            ContributorId = this.ContributorId,
        };
    #endregion
}