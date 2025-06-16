using Domain.Common;
using Domain.Posts;

namespace Domain.Categories;

public class Category(CategoryId id, string name) : Entity<CategoryId>(id), IAggregateRoot
{
    public new CategoryId Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = name.ToLower().Replace(" ", "-");

    private readonly List<Post> _posts = [];

    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();

    public void AddPost(Post post)
    {
        if (_posts.Contains(post))
            return;

        _posts.Add(post);
    }
}