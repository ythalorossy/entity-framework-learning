using Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CategoryRepository(DataDbContext dataDbContext) : ICategoryRepository
{
    public Task AddAsync(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);

        dataDbContext.Categories.Add(category);

        dataDbContext.SaveChanges();

        return Task.CompletedTask;
    }

    public async Task<Category> GetByIdAsync(CategoryId requestCategoryId)
    {
        return await dataDbContext.Categories.FindAsync(requestCategoryId);
    }

    public async Task<Category?> GetByNameAsync(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
            throw new ArgumentException("Category Name must be provided.", nameof(categoryName));

        return await dataDbContext.Categories
            .Where(c => c.Name == categoryName)
            .FirstOrDefaultAsync();
    }
}