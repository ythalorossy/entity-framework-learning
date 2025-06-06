using Domain.Authors;
using MediatR;

namespace Application.UseCases.Authors;

public record CreateAuthorCommand(string Name, string Email) : IRequest<AuthorId>;