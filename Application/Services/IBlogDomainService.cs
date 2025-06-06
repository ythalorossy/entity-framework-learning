using Domain.Blogs;

namespace Application.Services;

public interface IBlogDomainService
{
    void ValidateBlog(Blog blog);
}