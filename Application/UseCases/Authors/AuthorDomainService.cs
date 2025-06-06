using Application.Services;
using Domain.Authors;

namespace Application.UseCases.Authors;

public class AuthorDomainService(IAuthorRepository<AuthorId, Author> authorRepository) : IAuthorDomainService
{
    public void ValidateAuthor(Author author)
    {
        ArgumentNullException.ThrowIfNull(author);

        var existingAuthor = authorRepository.GetAuthorByNameAndEmailAsync(author.Name, author.Email);

        if (existingAuthor.Result != null)
            throw new InvalidOperationException("An author with the same name and email already exists.");
    }
}