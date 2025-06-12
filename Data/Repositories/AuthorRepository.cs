using Domain.Authors;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class AuthorRepository(DataDbContext dataDbContext) : IAuthorRepository<AuthorId, Author>
{
    public async Task<Author?> GetAuthorByIdAsync(AuthorId id)
    {
        return await dataDbContext.Authors
                   .FirstOrDefaultAsync(a => a != null && a.Id == id)
               ?? throw new InvalidOperationException($"Author with id {id} not found.");
    }


    public async Task<Author?> GetAuthorByNameAndEmailAsync(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name must be provided.", nameof(name));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email must be provided.", nameof(email));

        return await dataDbContext.Authors
            .Where(author => author != null && author.Name == name && author.Email == email)
            .FirstOrDefaultAsync();
    }

    public async Task<Author> AddAuthorAsync(Author? author)
    {
        ArgumentNullException.ThrowIfNull(author);
        await dataDbContext.Authors.AddAsync(author);
        await dataDbContext.SaveChangesAsync();
        return author;
    }
}