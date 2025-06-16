using Application.Interfaces;
using Domain.Categories;
using MediatR;

namespace Application.UseCases.Categories;

public record CreateCategoryCommand(string Name) : IRequest<CategoryId>;

public class CreateCategoryHandler(
    ICategoryRepository categoryRepository,
    ICategoryValidateService categoryValidateService)
    : IRequestHandler<CreateCategoryCommand, CategoryId>
{
    public async Task<CategoryId> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryId = CategoryId.New();
        var category = new Category(categoryId, request.Name);

        categoryValidateService.ValidateCategory(category);

        await categoryRepository.AddAsync(category);
        return categoryId;
    }
}