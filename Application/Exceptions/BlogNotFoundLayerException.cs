namespace Application.Exceptions;

public class BlogNotFoundLayerException(Guid blogId)
    : ApplicationLayerException($"BLog with ID '{blogId}' was not found.");