using Application.Exceptions;
using Domain.Blogs;
using MediatR;

namespace Application.UseCases.Blogs;

public record GetBlog(string BlogId) : IRequest<Blog>;

public class GetBlogHandler(IBlogRepository<BlogId, Blog> blogRepository) : IRequestHandler<GetBlog, Blog>
{
    public async Task<Blog> Handle(GetBlog request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.BlogId, out var parsedBlogId))
            throw new ValidationException(["Invalid blog ID format"]);
        
        var blogId = new BlogId(parsedBlogId);

        var blog = await blogRepository.GetBlogByIdAsync(blogId);

        return blog;
    }
}