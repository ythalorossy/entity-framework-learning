using Domain.Authors;
using Domain.Blogs;
using Domain.Common;
using Domain.Tags;

namespace Domain.Posts;

public class Post(PostId id, string title, string content) : Entity<PostId>(id)
{
    public string Title { get; set; } = title;
    public string Content { get; set; } = content;
    public DateTime PublishedOn { get; set; } = DateTime.UtcNow;
    public bool Archived { get; set; }

    // Blog relationship
    public BlogId BlogId { get; private set; } = null!;
    public Blog Blog { get; private set; } = null!;

    // Author relationship (composite foreign key)

    public AuthorId AuthorId { get; set; } = null!;
    public string AuthorEmail { get; set; } = null!;
    public Author Author { get; private set; } = null!;

    // Cloning relationship
    public PostId? ClonedFromId { get; set; } // Nullable to allow for posts that are not clones
    public Post? ClonedFrom { get; set; } // Nullable to allow for posts that are not clones

    public List<Tag> Tags { get; set; } = [];

    public Post SetAuthor(Author author)
    {
        Author = author;
        return this;
    }

    public Post SetBlog(Blog blog)
    {
        Blog = blog;
        return this;
    }
}