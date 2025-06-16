using Data.Configurations;
using Domain.Authors;
using Domain.Blogs;
using Domain.Categories;
using Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data;

/// <summary>
///     This is used when running Migrations and updating the Database
/// </summary>
public class DataDbContextFactory : IDesignTimeDbContextFactory<DataDbContext>
{
    public DataDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
        optionsBuilder
            .UseSqlServer(
                "Server=172.30.94.48, 1433;Database=entity-framework-learn;User Id=sa;Password=CSSAdmin99;TrustServerCertificate=True;",
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        10,
                        TimeSpan.FromSeconds(30),
                        null);
                }
            );

        return new DataDbContext(optionsBuilder.Options);
    }
}

public class DataDbContext(DbContextOptions<DataDbContext> options) : DbContext(options)
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new BlogConfiguration())
            .ApplyConfiguration(new PostConfiguration())
            .ApplyConfiguration(new AuthorConfiguration())
            .ApplyConfiguration(new CategoryConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder
                .UseSqlServer(
                    "Server=172.30.94.48,1433;Database=entity-framework-learn;User Id=sa;Password=CSSAdmin99;TrustServerCertificate=True;");
    }
}