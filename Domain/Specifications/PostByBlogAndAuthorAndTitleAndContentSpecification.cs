using System.Linq.Expressions;
using Domain.Authors;
using Domain.Blogs;
using Domain.Posts;

namespace Domain.Specifications;

public class PostByBlogAndAuthorAndTitleAndContentSpecification(
    Blog blog,
    Author author,
    string title,
    string content) : ISpecification<Post>
{
    public Expression<Func<Post?, bool>> Criteria =>
        post => post != null &&
                post.Blog == blog &&
                post.Author == author &&
                post.Title == title &&
                post.Content == content;
}