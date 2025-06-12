using Application.Exceptions;
using Application.Interfaces;
using Domain.Blogs;

namespace Application.Services;

public class BlogValidateService(
    IBlogRepository<BlogId, Blog> blogRepository) : IBlogValidateService
{
    public void ValidateBlog(Blog blog)
    {
        ArgumentNullException.ThrowIfNull(blog);

        var existingBlog = blogRepository.GetBlogByNameAndUriAsync(blog.Name, blog.SiteUri);

        if (existingBlog.Result != null)
            throw new ValidationException(["A blog with the same name and URI already exists."]);
    }
}