using Domain.Tags;

namespace Application.DTOs;

public class PostDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
    public Guid AuthorId { get; init; }
    public Guid BlogId { get; init; }
}