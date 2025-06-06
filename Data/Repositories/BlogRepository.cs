using Domain.Blogs;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BlogRepository(DataDbContext dataDbContext) : IBlogRepository<BlogId, Blog>
{
    public async Task<Blog> AddBlogAsync(Blog blog)
    {
        ArgumentNullException.ThrowIfNull(blog);
        await dataDbContext.Blogs.AddAsync(blog);
        await dataDbContext.SaveChangesAsync();
        return blog;
    }

    public async Task<Blog> GetBlogByNameAndUriAsync(string name, Uri siteUri)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name must be provided.", nameof(name));

        if (string.IsNullOrWhiteSpace(siteUri.ToString()))
            throw new ArgumentException("URI must be provided.", nameof(siteUri));

        return await dataDbContext.Blogs
                   .AsNoTracking()
                   .FirstOrDefaultAsync(b => b.Name == name && b.SiteUri == siteUri)
               ?? throw new InvalidOperationException("Blog not found with the given name and URI.");
    }


    public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
    {
        return await dataDbContext.Blogs
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Blog> GetBlogByIdAsync(BlogId id)
    {
        return await dataDbContext.Blogs.FindAsync(id)
               ?? throw new InvalidOperationException($"Blog with id {id} not found.");
    }

    public async Task<Blog> UpdateBlogAsync(Blog blog)
    {
        ArgumentNullException.ThrowIfNull(blog);

        var existingBlog = await dataDbContext.Blogs.FindAsync(blog.Id)
                           ?? throw new InvalidOperationException("Blog not found.");

        dataDbContext.Entry(existingBlog).CurrentValues.SetValues(blog);

        await dataDbContext.SaveChangesAsync();

        return existingBlog;
    }

    public Task DeleteBlogAsync(BlogId id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteBlogAsync(int id)
    {
        var blog = await dataDbContext.Blogs.FindAsync(id);
        if (blog != null)
        {
            dataDbContext.Blogs.Remove(blog);
            await dataDbContext.SaveChangesAsync();
        }
    }
}