using Application.Exceptions;
using Domain.Authors;
using Domain.Blogs;
using Domain.Categories;
using Domain.Posts;
using Domain.Specifications;
using MediatR;

namespace Application.UseCases.Posts;

public record CreatePost(
    string BlogId,
    string AuthorId,
    string CategoryId,
    string Title,
    string Content) : IRequest<PostId>;

public class CreatePostHandler(
    IAuthorRepository<AuthorId, Author> authorRepository,
    IBlogRepository<BlogId, Blog> blogRepository,
    IPostRepository<PostId, Post> postRepository,
    ICategoryRepository categoryRepository
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

        var categoryId = new CategoryId(Guid.Parse(request.CategoryId));
        var category = await categoryRepository.GetByIdAsync(categoryId)
                       ?? throw new CategoryNotFoundException(categoryId.Value);

        var existingPost = await postRepository.GetBySpecificationAsync(
            new PostByBlogAndAuthorAndCategoryAndTitleAndContentSpecification(blog, author, category, request.Title, request.Content));

        if (existingPost != null) throw new PostAlreadyExistsLayerException(existingPost.Id);

        var post = new Post(PostId.New(), request.Title, request.Content);
        post.SetAuthor(author);
        post.SetBlog(blog);
        post.SetCategory(category);

        await postRepository.AddPostAsync(post);

        return post.Id;
    }
}