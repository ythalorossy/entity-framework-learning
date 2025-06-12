namespace Application.Exceptions;

public class ApplicationLayerException : Exception
{
    protected ApplicationLayerException(string message) : base(message)
    {
    }
}