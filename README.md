##Solution Architecture

- Your solution follows a Clean Architecture structure with four projects:
  - `Api`: Presentation layer (Web API)
  - `Application`: Use cases, commands, handlers, services
  - `Domain`: Entities, value objects, interfaces, business rules
  - `Infra` (or `Data`): EF Core DbContext, repository implementations

##Application Layer and MediatR

- Use cases like `CreateBlogCommand` and `CreatePostCommand` are implemented in the `Application` project using MediatR.
- Handlers are injected with repository interfaces and domain services.
- MediatR is registered in `Program.cs` using:
  ```csharp
  builder.Services.AddMediatR(cfg =>
      cfg.RegisterServicesFromAssembly(typeof(CreateBlogCommand).Assembly));
  ```

## Dependency Injection and Service Registration

- Repository interfaces are defined in the Domain layer and implemented in the Infra layer.
- Services like `IBlogDomainService` are implemented in the Application layer.
- All services and repositories must be registered in `Program.cs` of the `Api` project:
  ```csharp
  builder.Services.AddScoped<IBlogRepository<BlogId, Blog>, BlogRepository>();
  builder.Services.AddScoped<IBlogDomainService, BlogDomainService>();
  ```

## Domain Services

- Domain services are used when logic spans multiple entities and doesn’t belong to a single one.
- Example: `IPostSchedulingService` checks if an author can schedule a post based on recent activity and blog limits.
- Domain service interfaces can be placed in `Application/Interfaces/Services`, and implementations in `Application/Services`.

## Exception Handling

- Custom exceptions like `AuthorNotFoundException` should inherit from a base `NotFoundException`:
  ```csharp
  public abstract class NotFoundException : Exception
  {
      protected NotFoundException(string entity, object key)
          : base($"{entity} with key '{key}' was not found.") {}
  }
  ```
- These should be placed in `Application/Exceptions`.
- Controllers should catch and return appropriate HTTP responses (e.g., 404 for not found).

## Entity Framework and Composite Keys

- EF Core composite keys require all key parts in `FindAsync`.
- Use `FirstOrDefaultAsync` with a predicate if you only have part of the key.
- Example:
  ```csharp
  var author = await dbContext.Authors
      .FirstOrDefaultAsync(a => a.Id == id);
  ```

## Circular Reference in JSON Responses

- Avoid returning EF entities directly from controllers.
- Use DTOs and AutoMapper to flatten and control the response shape.
- Configure JSON to ignore cycles (optional quick fix):
  ```csharp
  builder.Services.AddControllers().AddJsonOptions(options =>
      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
  ```

## AutoMapper Setup

- Install AutoMapper and register it in `Program.cs`:
  ```csharp
  builder.Services.AddAutoMapper(typeof(DomainToDtoProfile));
  ```
- Create DTOs in `Application/DTOs`.
- Create mapping profiles in `Application/Mapping`.

## Specification Pattern

- Define `ISpecification<T>` in the Domain layer.
- Implement concrete specifications in Domain or Application.
- Use a `SpecificationEvaluator` in the Infra layer to apply them to EF queries.
- Handle nullability issues by aligning expression types or casting.
