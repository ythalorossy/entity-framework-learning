using Application.Interfaces;
using Domain.Authors;
using MediatR;

namespace Application.UseCases.Authors;

public record CreateAuthor(string Name, string Email) : IRequest<AuthorId>;

public class CreateAuthorHandler(
    IAuthorValidateService authorValidateService,
    IAuthorRepository<AuthorId, Author> authorRepository) : IRequestHandler<CreateAuthor, AuthorId>
{
    public async Task<AuthorId> Handle(CreateAuthor request, CancellationToken cancellationToken)
    {
        var authorId = AuthorId.New();
        var name = request.Name;
        var email = request.Email;
        
        var author = new Author(authorId, name, email);

        authorValidateService.ValidateAuthor(author);

        await authorRepository.AddAuthorAsync(author);
        return authorId;
    }
}