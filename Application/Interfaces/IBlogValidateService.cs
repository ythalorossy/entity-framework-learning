using Domain.Blogs;

namespace Application.Interfaces;

public interface IBlogValidateService
{
    void ValidateBlog(Blog blog);
}