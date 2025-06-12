namespace Domain.Specifications;

public interface IRepositorySpecification<TEntity>
{
    public Task<TEntity?> GetBySpecificationAsync(ISpecification<TEntity> spec);
}