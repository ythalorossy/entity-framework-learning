using Application.Exceptions;
using Domain.Authors;
using Domain.Blogs;
using Domain.Posts;
using Domain.Specifications;
using MediatR;

namespace Application.UseCases.Posts;

public record CreatePost(
    string BlogId,
    string AuthorId,
    string Title,
    string Content) : IRequest<PostId>;
    
    
public class CreatePostHandler(
    IAuthorRepository<AuthorId, Author> authorRepository,
    IBlogRepository<BlogId, Blog> blogRepository,
    IPostRepository<PostId, Post> postRepository
) : IRequestHandler<CreatePost, PostId>
{
    public async Task<PostId> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        var authorId = new AuthorId(Guid.Parse(request.AuthorId));

        var author = await authorRepository.GetAuthorByIdAsync(authorId)
                     ?? throw new AuthorNotFoundLayerException(authorId.Value);

        var blogId = new BlogId(Guid.Parse(request.BlogId));

        var blog = await blogRepository.GetBlogByIdAsync(blogId)
                   ?? throw new BlogNotFoundLayerException(blogId.Value);

        var existingPost = await postRepository.GetBySpecificationAsync(
            new PostByBlogAndAuthorAndTitleAndContentSpecification(blog, author, request.Title, request.Content));

        if (existingPost != null) throw new PostAlreadyExistsLayerException(existingPost.Id);

        var post = new Post(PostId.New(), request.Title, request.Content);
        post.SetAuthor(author);
        post.SetBlog(blog);

        await postRepository.AddPostAsync(post);

        return post.Id;
    }
}    