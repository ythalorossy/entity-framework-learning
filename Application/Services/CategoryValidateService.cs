using Application.Exceptions;
using Application.Interfaces;
using Domain.Categories;

namespace Application.Services;

public class CategoryValidateService(
    ICategoryRepository categoryRepository) : ICategoryValidateService
{
    public void ValidateCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);

        var existingCategory = categoryRepository.GetByNameAsync(category.Name);

        if (existingCategory.Result != null)
            throw new ValidationException(["A category with the same name already exists."]);
    }
}