using D = YyCollection.Server.DomainService.Posts.Entities;
using S = YyCollection.DataStore.Rdb.Core.Entities.Tables;

namespace YyCollection.Server.DomainService.Posts.Converters;

/// <summary>
/// 投稿のレイヤー変換を提供します。
/// </summary>
public static class PostConverter
{
    #region Domain Entity -> Rdb
    /// <summary>
    /// Post の Rdb に変換します。
    /// </summary>
    /// <param name="post"></param>
    /// <returns></returns>
    public static S.Post ToRdb(this D.Post post)
        => new()
        {
            Id = post.Id,
            Title = post.Title,
            Overview = post.Overview,
            MediaUrl = post.MediaUrl,
            StartTime = post.StartTime,
            EndTime = post.EndTime,
            ContributorId = post.ContributorId,
        };
    #endregion
}