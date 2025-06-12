using Application.Exceptions;
using Domain.Blogs;
using Domain.Posts;
using MediatR;

namespace Application.UseCases.Posts;

public record GetPostsForBlog(BlogId BlogId) : IRequest<IList<Post>>;

public class GetPostsForBlogHandler(
    IBlogRepository<BlogId, Blog> blogRepository,
    IPostRepository<PostId, Post> postRepository)
    : IRequestHandler<GetPostsForBlog, IList<Post>>
{
    public async Task<IList<Post>> Handle(GetPostsForBlog request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetBlogByIdAsync(request.BlogId)
                   ?? throw new BlogNotFoundLayerException(request.BlogId.Value);

        var posts = await postRepository.GetPostsForBlogAsync(blog.Id);

        return posts;
    }
}