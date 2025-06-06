using Domain.Authors;

namespace Application.Services;

public interface IAuthorDomainService
{
    void ValidateAuthor(Author author);
}