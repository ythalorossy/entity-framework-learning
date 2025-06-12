using Application.Exceptions;
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
        try
        {
            var postId = await mediator.Send(command);
            return Ok(postId);
        }
        catch (ApplicationLayerException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", ex });
        }
    }

    [HttpPost]
    [Route("clone")]
    public async Task<IActionResult> Clone([FromBody] ClonePost command)
    {
        try
        {
            var postId = await mediator.Send(command);
            return Ok(postId);
        }
        catch (ApplicationLayerException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", ex });
        }
    }
}