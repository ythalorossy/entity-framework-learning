using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.UseCases.Authors;
using Data.Repositories;
using Domain.Authors;
using Moq;
using NUnit.Framework;

namespace Tests.Integration.Application.UseCases.Authors;

[TestFixture]
public class AuthorIntegrationTests : TestDatabaseFixture
{
    private Mock<IAuthorValidateService> _authorValidateService;
    private IAuthorRepository<AuthorId, Author> _authorRepository;
    private CreateAuthorHandler _handler;

    [SetUp]
    public void Setup()
    {
        var context = CreateContext();
        _authorRepository = new AuthorRepository(context);
        _authorValidateService = new Mock<IAuthorValidateService>();
        _handler = new CreateAuthorHandler(_authorValidateService.Object, _authorRepository);
    }

    [Test]
    public async Task Handle_ValidAuthor_ShouldCreateAndReturnId()
    {
        // Arrange
        var command = new CreateAuthor("Test Author", "test@example.com");
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await using var context = CreateContext();
        var savedAuthor = await context.Authors.FindAsync(result, command.Email);
        
        Assert.That(savedAuthor, Is.Not.Null);
        Assert.That(savedAuthor.Name, Is.EqualTo(command.Name));
        Assert.That(savedAuthor.Email, Is.EqualTo(command.Email));
        
        _authorValidateService.Verify(s => 
            s.ValidateAuthor(It.Is<Author>(a => 
                a.Name == command.Name && 
                a.Email == command.Email)), 
            Times.Once);
    }

    [Test]
    public async Task Handle_DuplicateAuthor_ShouldThrowException()
    {
        // Arrange
        var command = new CreateAuthor("Test Author", "test@example.com");
        
        _authorValidateService
            .Setup(s => s.ValidateAuthor(It.IsAny<Author>()))
            .Throws(new InvalidOperationException("An author with the same name and email already exists."));

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => 
            await _handler.Handle(command, CancellationToken.None));
        
        Assert.That(ex.Message, Is.EqualTo("An author with the same name and email already exists."));
    }
}