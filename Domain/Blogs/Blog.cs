using Domain.Common;
using Domain.Posts;

namespace Domain.Blogs;

public class Blog(BlogId id, string name, Uri siteUri) : Entity<BlogId>(id), IAggregateRoot
{
    private readonly List<Post> _posts = [];
    public string Name { get; private set; } = name;
    public Uri SiteUri { get; private set; } = siteUri;
    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();

    public void AddPost(Post post)
    {
        var existingPost = _posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost == null) _posts.Add(post);
    }
}