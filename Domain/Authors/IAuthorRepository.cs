namespace Domain.Authors;

public interface IAuthorRepository<in TId, TEntity>
{
    Task<TEntity?> GetAuthorByIdAsync(TId id);

    Task<TEntity?> GetAuthorByNameAndEmailAsync(string name, string email);

    Task<TEntity> AddAuthorAsync(TEntity author);
}