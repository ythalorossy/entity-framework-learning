namespace Domain.Blogs;

public interface IBlogRepository<in TId, TEntity>
{
    Task<TEntity> GetBlogByIdAsync(TId id);
    Task<TEntity?> GetBlogByNameAndUriAsync(string name, Uri siteUri);
    Task<IEnumerable<TEntity>> GetAllBlogsAsync();
    Task<TEntity> AddBlogAsync(TEntity blog);
    Task<TEntity> UpdateBlogAsync(TEntity blog);
    Task DeleteBlogAsync(TId id);
}