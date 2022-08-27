using System.Text.Json.Serialization;

namespace YyCollection.Server.Models.Posts;

/// <summary>
/// /Post/ の API レスポンスを表します。
/// </summary>
public sealed class PostResponse
{
    #region プロパティ
    /// <summary>
    /// 投稿 ID
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }
    
    
    /// <summary>
    /// 投稿タイトル
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("title")]
    public string Title { get; init; }
    
    
    /// <summary>
    /// 投稿者 ID
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("contributorId")]
    public Ulid ContributorId { get; init; }
    
    
    /// <summary>
    /// 投稿者アイコン URL
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("contributorIconUrl")]
    public string ContributorIconUrl { get; init; }
    
    
    /// <summary>
    /// 投稿者名
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("contributorName")]
    public string ContributorName { get; init; }
    
    
    /// <summary>
    /// 投稿説明
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("description")]
    public string Description { get; init; }


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
    /// 投稿日
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("postDate")]
    public DateOnly PostDate { get; init; }
    #endregion


    #region コンストラクタ
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="id"></param>
    /// <param name="title"></param>
    /// <param name="contributorId"></param>
    /// <param name="contributorName"></param>
    /// <param name="contributorIconUrl"></param>
    /// <param name="description"></param>
    /// <param name="mediaUrl"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="postDate"></param>
    public PostResponse(Ulid id, string title, Ulid contributorId, string contributorName, string contributorIconUrl, string description, string mediaUrl, int startTime, int endTime, DateOnly postDate)
    {
        this.Id = id;
        this.Title = title;
        this.ContributorId = contributorId;
        this.ContributorName = contributorName;
        this.ContributorIconUrl = contributorIconUrl;
        this.Description = description;
        this.MediaUrl = mediaUrl;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.PostDate = postDate;
    }
    #endregion
}