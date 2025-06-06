using Application.Services;
using Domain.Authors;
using MediatR;

namespace Application.UseCases.Authors;

public class CreateAuthorHandler(
    IAuthorDomainService authorDomainService,
    IAuthorRepository<AuthorId, Author> authorRepository) : IRequestHandler<CreateAuthorCommand, AuthorId>
{
    public async Task<AuthorId> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var name = request.Name;
        var email = request.Email;
        var authorId = AuthorId.New();
        var author = new Author(authorId, name, email);

        authorDomainService.ValidateAuthor(author);

        await authorRepository.AddAuthorAsync(author);
        return authorId;
    }
}