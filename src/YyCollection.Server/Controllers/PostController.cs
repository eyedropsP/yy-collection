using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YyCollection.Server.DomainService.Posts;

namespace YyCollection.Server.Controllers;

/// <summary>
/// 投稿関連の API を提供します。
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class PostsController : ControllerBase
{
    /// <summary>
    /// 指定された投稿を取得します。
    /// </summary>
    /// <param name="postService"></param>
    /// <param name="postId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("{postId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> GetPostById(
        [FromServices] PostService postService,
        [FromRoute] Ulid postId)
    {
        return this.Ok();
    }


    /// <summary>
    /// 投稿を指定された条件で取得します。
    /// </summary>
    /// <param name="postService"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async ValueTask<IActionResult> GetPosts(
        [FromServices] PostService postService,
        int limit,
        int offset)
    {
        return this.Ok();
    }
}