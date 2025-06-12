using Domain.Blogs;
using Domain.Posts;
using Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PostRepository(DataDbContext dataDbContext)
    : IPostRepository<PostId, Post>
{
    public async Task<Post?> GetBySpecificationAsync(ISpecification<Post> spec)
    {
        return await dataDbContext.Posts.FirstOrDefaultAsync(spec.Criteria);
    }

    public async Task<IList<Post>> GetPostsForBlogAsync(BlogId blogId)
    {
        return await dataDbContext.Posts
            .Where(p => p.BlogId == blogId)
            .ToListAsync();
    }

    public async Task<Post> AddPostAsync(Post post)
    {
        await dataDbContext.Posts.AddAsync(post);
        await dataDbContext.SaveChangesAsync();
        return post;
    }

    public async Task DeletePostAsync(PostId id)
    {
        await dataDbContext.Posts
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<List<Post>> GetAllPostsAsync()
    {
        return await dataDbContext.Posts
            .ToListAsync();
    }

    public async Task<Post> GetPostByIdAsync(PostId id) // Updated return type to match interface
    {
        return await dataDbContext.Posts
                   .Include(p => p.Author)
                   .Include(p => p.Blog)
                   .FirstOrDefaultAsync(p => p.Id == id)
               ?? throw new InvalidOperationException($"Post with ID {id} not found.");
    }

    public async Task<Post> UpdatePostAsync(Post post)
    {
        var existingPost = await dataDbContext.Posts.FirstOrDefaultAsync(p => p.Id == post.Id)
                           ?? throw new InvalidOperationException($"Post with ID {post.Id} not found.");

        existingPost.Title = post.Title;
        existingPost.Content = post.Content;
        existingPost.PublishedOn = post.PublishedOn;
        existingPost.Archived = post.Archived;

        await dataDbContext.SaveChangesAsync();

        return existingPost;
    }
}