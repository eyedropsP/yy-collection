using YyCollection.Client.DomainServices.Posts;

namespace YyCollection.Client.Apis;

/// <summary>
/// Post 関連の API 呼び出しを提供します。
/// </summary>
public class PostApiV1
{
    #region プロパティ
    /// <summary>
    /// API のエンドポイントを取得します。
    /// </summary>
    private Endpoint EndPoint { get; }

    private static HttpClient Client { get; } = new();
    #endregion


    #region コンストラクタ
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="endPoint"></param>
    public PostApiV1(Endpoint endPoint)
    {
        EndPoint = endPoint;
    }
    #endregion

    /// <summary>
    /// ID と一致する投稿を取得します。
    /// </summary>
    public async ValueTask<Post> GetByIdAsync(Ulid id, CancellationToken cancellationToken)
    {
        Post? post = default;
        
        var uri = $"api/v1/post/{id.ToString()}";
        var requestUri = new Uri(uri);
        var response = await Client.GetAsync(requestUri, cancellationToken);
        // if (response.IsSuccessStatusCode)
            // post = await response.Content.ReadFromJsonAsync<Post>(cancellationToken);

        return post;
    }

    /// <summary>
    /// 投稿を指定された条件で取得します。
    /// </summary>
    public void GetPostsAsync()
    {
    }

    /// <summary>
    /// 投稿します。
    /// </summary>
    public void PostAsync()
    {
    }

    /// <summary>
    /// 投稿を更新します。
    /// </summary>
    public void UpdatePostAsync()
    {
    }

    /// <summary>
    /// 投稿を削除します。
    /// </summary>
    public void DeletePostAsync()
    {
    }
}