using Application.UseCases.Posts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController(IMediator mediator) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePost command)
    {
        var postId = await mediator.Send(command);
        return Ok(postId);
    }

    [HttpPost]
    [Route("clone")]
    public async Task<IActionResult> Clone([FromBody] ClonePost command)
    {
        var postId = await mediator.Send(command);
        return Ok(postId);
    }
    
    [HttpPost]
    [Route("{postId}/categories")]
    public async Task<IActionResult> AddCategory(string postId, [FromBody] AddCategoryToPost command)
    {
        await mediator.Send(command);
        return Ok();
    }

}