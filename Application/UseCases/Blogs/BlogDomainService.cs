using Application.Services;
using Domain.Blogs;

namespace Application.UseCases.Blogs;

public class BlogDomainService(
    IBlogRepository<BlogId, Blog> blogRepository) : IBlogDomainService
{
    public void ValidateBlog(Blog blog)
    {
        ArgumentNullException.ThrowIfNull(blog);

        var existingBlog = blogRepository.GetBlogByNameAndUriAsync(blog.Name, blog.SiteUri);

        if (existingBlog != null)
            throw new InvalidOperationException("A blog with the same name and URI already exists.");
    }
}