using Application.Interfaces;
using Domain.Blogs;
using MediatR;

namespace Application.UseCases.Blogs;

public record CreateBlog(Guid? Id, string Name, string SiteUri) : IRequest<BlogId>;

public class CreateBlogHandler(
    IBlogRepository<BlogId, Blog> blogRepository,
    IBlogValidateService blogValidateService
) : IRequestHandler<CreateBlog, BlogId>
{
    public async Task<BlogId> Handle(CreateBlog request, CancellationToken cancellationToken)
    {
        var blogId = BlogId.New();
        var blog = new Blog(BlogId.New(), request.Name, new Uri(request.SiteUri));
        blogValidateService.ValidateBlog(blog);
        await blogRepository.AddBlogAsync(blog);
        return blogId;
    }
}