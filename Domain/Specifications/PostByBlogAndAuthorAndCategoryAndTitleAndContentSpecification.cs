using System.Linq.Expressions;
using Domain.Authors;
using Domain.Blogs;
using Domain.Categories;
using Domain.Posts;

namespace Domain.Specifications;

public class PostByBlogAndAuthorAndCategoryAndTitleAndContentSpecification(
    Blog blog,
    Author author,
    Category category,
    string title,
    string content) : ISpecification<Post>
{
    public Expression<Func<Post?, bool>> Criteria =>
        post => post != null &&
                post.Blog == blog &&
                post.Author == author &&
                post.Category == category &&
                post.Title == title &&
                post.Content == content;
}