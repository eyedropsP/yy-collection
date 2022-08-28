using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValueTaskSupplement;
using YyCollection.Server.DomainService.Categories;
using YyCollection.Server.DomainService.Posts;
using YyCollection.Server.DomainService.Posts.Entities;
using YyCollection.Server.DomainService.Tags;
using YyCollection.Server.Models.Posts;

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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("{postId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> GetPostById(
        [FromServices] PostService postService,
        [FromRoute] Ulid postId,
        CancellationToken cancellationToken)
    {
        return this.Ok();
    }

    /// <summary>
    /// 投稿を指定された条件で取得します。
    /// </summary>
    /// <param name="postService"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="cancellationToken"></param>
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
        int offset,
        CancellationToken cancellationToken)
    {
        return this.Ok();
    }

    /// <summary>
    /// 切り抜き動画を投稿します。
    /// </summary>
    /// <param name="postService"></param>
    /// <param name="categoryService"></param>
    /// <param name="tagService"></param>
    /// <param name="postRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async ValueTask<IActionResult> Post(
        [FromServices] PostService postService,
        [FromServices] CategoryService categoryService,
        [FromServices] TagService tagService,
        [FromBody] PostRequest postRequest,
        CancellationToken cancellationToken)
    {
        var categoryId = postRequest.CategoryId;
        var tagIds = postRequest.TagIds;
        var (category, tags) =
            await ValueTaskEx.WhenAll
            (
                categoryService.GetAsync(categoryId, cancellationToken: cancellationToken),
                tagService.GetAsync(tagIds, cancellationToken: cancellationToken)
            );
        var post = postRequest.ToDomain();

        if (category is null)
            return this.BadRequest("カテゴリが不正です。");

        var postRelation = new PostRelation()
        {
            Post = post,
            Category = category,
            Tags = tags,
        };

        var success = await postService.PostAsync(postRelation, cancellationToken: cancellationToken);
        if (!success)
            return this.BadRequest("投稿に失敗しました。");

        return this.Ok(success);
    }

    /// <summary>
    /// 切り抜き動画を更新します。
    /// </summary>
    /// <param name="postService"></param>
    /// <param name="tagService"></param>
    /// <param name="postRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="categoryService"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async ValueTask<IActionResult> UpdatePost(
        [FromServices] PostService postService,
        [FromServices] CategoryService categoryService,
        [FromServices] TagService tagService,
        [FromBody] PostRequest postRequest,
        CancellationToken cancellationToken)
    {
        var categoryId = postRequest.CategoryId;
        var tagIds = postRequest.TagIds;
        var (category, tags) =
            await ValueTaskEx.WhenAll
            (
                categoryService.GetAsync(categoryId, cancellationToken: cancellationToken),
                tagService.GetAsync(tagIds, cancellationToken: cancellationToken)
            );
        var post = postRequest.ToDomain();

        if (category is null)
            return this.BadRequest("カテゴリが不正です。");

        var postRelation = new PostRelation()
        {
            Post = post,
            Category = category,
            Tags = tags,
        };
        var success = await postService.UpdateAsync(postRelation, cancellationToken: cancellationToken);
        if (!success)
            return this.BadRequest("更新に失敗しました。");

        return this.Ok(success);
    }

    /// <summary>
    /// 切り抜き動画を削除します。
    /// </summary>
    /// <param name="postService"></param>
    /// <param name="postId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{postId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async ValueTask<IActionResult> DeletePost(
        [FromServices] PostService postService,
        [FromRoute] Ulid postId,
        CancellationToken cancellationToken)
    {
        var success = await postService.DeleteAsync(postId, cancellationToken: cancellationToken);
        if (!success)
            return this.BadRequest("削除に失敗しました。");

        return this.Ok(success);
    }
}