#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.UseCases.Authors;
using Domain.Authors;
using Moq;
using NUnit.Framework;

namespace Tests.Unit.Application.UseCases.Authors;

[TestFixture]
[TestOf(typeof(CreateAuthorHandler))]
public class CreateAuthorHandlerTests
{
    private Mock<IAuthorValidateService> _authorValidateService;
    private Mock<IAuthorRepository<AuthorId, Author>> _authorRepository;
    private CreateAuthorHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _authorValidateService = new Mock<IAuthorValidateService>();
        _authorRepository = new Mock<IAuthorRepository<AuthorId, Author>>();
        
        _handler = new CreateAuthorHandler(_authorValidateService.Object, _authorRepository.Object);
    }
    
    [Test]
    public async Task Handle_ValidAuthor_ShouldCreateAndReturnId()
    {
        // Arrange
        var command = new CreateAuthor("John Doe", "john@example.com");
        Author? savedAuthor = null;
        
        _authorRepository
            .Setup(r => r.AddAuthorAsync(It.IsAny<Author>()))
            .Callback<Author>(author => savedAuthor = author) // Capture the author
            .ReturnsAsync((Author author) => author);         // Return the same author

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value, Is.Not.EqualTo(Guid.Empty));
        
        _authorValidateService.Verify(
            s => s.ValidateAuthor(It.Is<Author>(a => 
                a.Name == command.Name 
                && a.Email == command.Email)),
            Times.Once);
            
        Assert.That(savedAuthor, Is.Not.Null);
        Assert.That(savedAuthor?.Name, Is.EqualTo(command.Name));
        Assert.That(savedAuthor?.Email, Is.EqualTo(command.Email));
        Assert.That(result, Is.EqualTo(savedAuthor?.Id));
    }
    
    [Test]
    public void Handle_ValidationFails_ShouldThrowException()
    {
        // Arrange
        var command = new CreateAuthor("John Doe", "john@example.com");
        
        _authorValidateService
            .Setup(s => s.ValidateAuthor(It.IsAny<Author>()))
            .Throws(new InvalidOperationException("Validation failed"));

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
            
        _authorRepository.Verify(
            r => r.AddAuthorAsync(It.IsAny<Author>()),
            Times.Never);
    }
}