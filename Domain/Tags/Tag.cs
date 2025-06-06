using Domain.Posts;

namespace Domain.Tags;

public class Tag
{
    public int Id { get; init; }
    public List<Post> Posts { get; init; } = [];
}