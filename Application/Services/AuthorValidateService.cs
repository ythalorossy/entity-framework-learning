using Application.Interfaces;
using Domain.Authors;

namespace Application.UseCases.Authors;

public class AuthorValidateService(IAuthorRepository<AuthorId, Author> authorRepository) : IAuthorValidateService
{
    public void ValidateAuthor(Author author)
    {
        ArgumentNullException.ThrowIfNull(author);

        var existingAuthor = authorRepository.GetAuthorByNameAndEmailAsync(author.Name, author.Email);

        if (existingAuthor.Result != null)
            throw new InvalidOperationException("An author with the same name and email already exists.");
    }
}