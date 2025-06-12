using Application.Exceptions;
using Domain.Posts;
using MediatR;

namespace Application.UseCases.Posts;

public record ClonePost(string PostId) : IRequest<PostId>;

public class ClonePostHandler(IPostRepository<PostId, Post> postRepository) : IRequestHandler<ClonePost, PostId>
{
    public async Task<PostId> Handle(ClonePost request, CancellationToken cancellationToken)
    {
        var postId = new PostId(Guid.Parse(request.PostId));

        var postToBeCloned = await postRepository.GetPostByIdAsync(postId)
                             ?? throw new PostNotFoundException(postId.Value);

        var postClone = new Post(PostId.New(), $"Cloned - {postToBeCloned.Title}", postToBeCloned.Content);
        postClone.SetAuthor(postToBeCloned.Author);
        postClone.SetBlog(postToBeCloned.Blog);
        postClone.ClonedFrom = postToBeCloned;

        await postRepository.AddPostAsync(postClone);
        return postClone.Id;
    }
}