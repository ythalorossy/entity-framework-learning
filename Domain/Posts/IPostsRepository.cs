namespace Domain.Posts;

public interface IPostsRepository<in TId, TEntity>
{
    /// <summary>
    ///     Gets the post by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the post.</param>
    /// <returns>The post with the specified identifier.</returns>
    Task<TEntity> GetPostByIdAsync(TId id);

    /// <summary>
    ///     Gets all posts.
    /// </summary>
    /// <returns>A list of all posts.</returns>
    Task<IEnumerable<TEntity>> GetAllPostsAsync();

    /// <summary>
    ///     Adds a new post.
    /// </summary>
    /// <param name="post">The post to add.</param>
    Task<TEntity> AddPostAsync(TEntity post);

    /// <summary>
    ///     Updates an existing post.
    /// </summary>
    /// <param name="post">The post to update.</param>
    Task<TEntity> UpdatePostAsync(TEntity post);

    /// <summary>
    ///     Deletes a post by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the post to delete.</param>
    Task DeletePostAsync(TId id);
}