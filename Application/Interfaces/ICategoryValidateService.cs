using Domain.Categories;

namespace Application.Interfaces;

public interface ICategoryValidateService
{
    void ValidateCategory(Category category);
}