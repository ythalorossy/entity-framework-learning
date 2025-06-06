using Domain.Blogs;
using MediatR;

namespace Application.UseCases.Blogs;

public record CreateBlogCommand(Guid? Id, string Name, string SiteUri) : IRequest<BlogId>;