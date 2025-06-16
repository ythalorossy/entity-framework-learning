using Application.Exceptions;
using Domain.Categories;
using Domain.Posts;
using MediatR;

namespace Application.UseCases.Posts;

public record AddCategoryToPost(string PostId, string CategoryId) : IRequest;

public class AddCategoryToPostHandler(
    IPostRepository<PostId, Post> postRepository,
    ICategoryRepository categoryRepository)
    : IRequestHandler<AddCategoryToPost>
{
    public async Task Handle(AddCategoryToPost request, CancellationToken cancellationToken)
    {
        var postId = new PostId(Guid.Parse(request.PostId));
        
        var post = await postRepository.GetPostByIdAsync(postId)
                   ?? throw new PostNotFoundException(postId.Value);

        var categoryId = new CategoryId(Guid.Parse(request.CategoryId));
        
        var category = await categoryRepository.GetByIdAsync(categoryId)
                       ?? throw new CategoryNotFoundException(categoryId.Value);

        post.SetCategory(category);
        await postRepository.UpdatePostAsync(post);
    }
}