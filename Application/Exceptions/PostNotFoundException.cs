using Domain.Posts;

namespace Application.Exceptions;

public class PostNotFoundException(Guid postId)
    : ApplicationLayerException($"Post with ID '{postId}' was not found");