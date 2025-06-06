using Application.Services;
using Domain.Blogs;
using MediatR;

namespace Application.UseCases.Blogs;

public class CreateBlogHandler(
    IBlogRepository<BlogId, Blog> blogRepository,
    IBlogDomainService blogDomainService
) : IRequestHandler<CreateBlogCommand, BlogId>
{
    public async Task<BlogId> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        // Business logic 
        var blogId = request.Id.HasValue ? new BlogId(request.Id.Value) : BlogId.New();
        var siteUri = new Uri(request.SiteUri);

        var blog = new Blog(blogId, request.Name, siteUri);

        blogDomainService.ValidateBlog(blog);

        // Save to DB via repository (injected)
        await blogRepository.AddBlogAsync(blog);
        return blogId;
    }
}