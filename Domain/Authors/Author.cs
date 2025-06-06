using Domain.Common;
using Domain.Posts;

namespace Domain.Authors;

public class Author(AuthorId id, string name, string email) : Entity<AuthorId>(id)
{
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
    public ICollection<Post> Posts { get; } = [];
}