using Domain.Authors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors");

        builder.HasKey(a => new
        {
            a.Id,
            a.Email
        });

        builder.Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => new AuthorId(value));

        builder.HasMany(a => a.Posts)
            .WithOne(p => p.Author)
            .HasPrincipalKey(a => new { a.Id, a.Email })
            .HasForeignKey(p => new { p.AuthorId, p.AuthorEmail })
            .IsRequired();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.CreatedOn).HasConversion(
            v => v,
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
        );
    }
}