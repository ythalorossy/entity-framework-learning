namespace Domain.Authors;

public interface IAuthorRepository<in TId, TEntity>
{
    Task<TEntity?> GetAuthorByNameAndEmailAsync(string name, string email);

    Task<TEntity> AddAuthorAsync(TEntity author);
}