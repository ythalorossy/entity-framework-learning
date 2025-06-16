namespace Domain.Categories;

public interface ICategoryRepository
{
    Task AddAsync(Category category);
    Task<Category> GetByIdAsync(CategoryId requestCategoryId);
    Task<Category?> GetByNameAsync(string categoryName);
}