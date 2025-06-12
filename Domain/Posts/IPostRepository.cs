using Domain.Blogs;
using Domain.Specifications;

namespace Domain.Posts;

public interface IPostRepository<in TId, TEntity> : IRepositorySpecification<Post>
{
    Task<TEntity> GetPostByIdAsync(TId id);

    Task<IList<Post>> GetPostsForBlogAsync(BlogId blogId);

    Task<List<Post>> GetAllPostsAsync();

    Task<TEntity> AddPostAsync(TEntity post);

    Task<TEntity> UpdatePostAsync(TEntity post);

    Task DeletePostAsync(TId id);
}