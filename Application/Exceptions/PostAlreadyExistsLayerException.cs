using Domain.Posts;

namespace Application.Exceptions;

public class PostAlreadyExistsLayerException(PostId postId)
    : ApplicationLayerException($"Post already exists with ID '{postId}'.");