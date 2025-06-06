using Domain.Blogs;
using Domain.Posts;
using Domain.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        // Configure primary key with value converter
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => new PostId(value)
            );

        // Configure foreign key BlogId with value converter
        builder.Property(p => p.BlogId)
            .HasConversion(
                id => id.Value,
                value => new BlogId(value)
            );

        builder.HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .HasForeignKey(p => p.BlogId)
            .IsRequired();

        // Configure ClonedFromId if it's a value object
        builder.Property(p => p.ClonedFromId)
            .HasConversion(
                id => id!.Value,
                value => new PostId(value)
            );

        builder.HasOne(p => p.ClonedFrom)
            .WithMany()
            .HasForeignKey(p => p.ClonedFromId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure many-to-many with Tags
        builder.HasMany(p => p.Tags)
            .WithMany(t => t.Posts)
            .UsingEntity("PostTag",
                right => right.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagsId").HasPrincipalKey(nameof(Tag.Id)),
                left => left.HasOne(typeof(Post)).WithMany().HasForeignKey("PostId").HasPrincipalKey(nameof(Post.Id)),
                join => join.HasKey("PostId", "TagsId")
            );

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Content)
            .IsRequired();

        builder.Property(p => p.PublishedOn)
            .IsRequired();

        builder.Property(p => p.Archived)
            .HasDefaultValue(false);
    }
}