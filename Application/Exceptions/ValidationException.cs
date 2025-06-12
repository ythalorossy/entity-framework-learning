namespace Application.Exceptions;

public class ValidationException(IEnumerable<string> errors)
    : ApplicationLayerException("One or more validation failures have occurred.")
{
    public IEnumerable<string> Errors { get; } = errors;
}