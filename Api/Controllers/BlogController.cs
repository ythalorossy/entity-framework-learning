using Application.DTOs;
using Application.Exceptions;
using Application.UseCases.Blogs;
using Application.UseCases.Posts;
using AutoMapper;
using Domain.Blogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ValidationException = Application.Exceptions.ValidationException;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BlogController(IMediator mediator, IMapper mapper)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    [HttpPost]
    [ProducesResponseType(typeof(BlogId), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlog command)
    {
        var blogId = await _mediator.Send(command);
        return Ok(blogId);
    }

    [HttpGet]
    [Route("{blogId}")]
    public async Task<IActionResult> GetBlog(string blogId)
    {
        var blog = await _mediator.Send(new GetBlog(blogId));
        return Ok(_mapper.Map<BlogDto>(blog));
    }

    [HttpGet]
    [Route("{blogId}/posts")]
    public async Task<IActionResult> GetPosts(string blogId)
    {
        if (!Guid.TryParse(blogId, out var parsedBlogId))
            throw new ValidationException(["Invalid blog ID format"]);

        var posts = await _mediator.Send(new GetPostsForBlog(new BlogId(parsedBlogId)));

        if (!posts.Any())
            throw new BlogNotFoundLayerException(parsedBlogId);

        return Ok(_mapper.Map<IEnumerable<PostDto>>(posts));
    }
}