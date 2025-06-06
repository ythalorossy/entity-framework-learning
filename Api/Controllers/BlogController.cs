using Application.UseCases.Blogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController(IMediator mediator) : Controller
{
    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        return Ok(await Task.FromResult("Hello World!"));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogCommand command)
    {
        try
        {
            var blogId = await mediator.Send(command);
            return Ok(blogId);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // Optional: log the exception
            return StatusCode(500, new { message = "An unexpected error occurred.", ex });
        }
    }
}