namespace Application.Exceptions;

public class CategoryNotFoundException(Guid categoryId)
    : ApplicationLayerException($"Category with ID '{categoryId}' was not found");