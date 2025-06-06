using Application.UseCases.Authors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController(IMediator mediator) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAuthorCommand command)
    {
        try
        {
            var authorId = await mediator.Send(command);
            return Ok(authorId);
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