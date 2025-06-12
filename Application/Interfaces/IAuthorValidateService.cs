using Domain.Authors;

namespace Application.Interfaces;

public interface IAuthorValidateService
{
    void ValidateAuthor(Author author);
}