namespace Application.Exceptions;

public class AuthorNotFoundLayerException(Guid authorId)
    : ApplicationLayerException($"Author with ID '{authorId}' was not found.");