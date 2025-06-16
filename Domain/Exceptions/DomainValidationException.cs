namespace Domain.Exceptions;

public class DomainValidationException(string categoryNameCannotBeEmpty)
    : Exception(categoryNameCannotBeEmpty)
{
}