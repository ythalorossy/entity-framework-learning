namespace Application.DTOs;

public class BlogDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required Uri SiteUri { get; init; }
    public List<PostDto> Posts { get; init; } = [];
}