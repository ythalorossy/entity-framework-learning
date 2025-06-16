using System;
using Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.Integration;

public class TestDatabaseFixture
{
    private DbContextOptions<DataDbContext> Options { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var dbName = $"TestDb_{Guid.NewGuid()}";
        Options = new DbContextOptionsBuilder<DataDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        // Create and seed the database
        using var context = new DataDbContext(Options);
        context.Database.EnsureCreated();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        using var context = new DataDbContext(Options);
        context.Database.EnsureDeleted();
    }

    protected DataDbContext CreateContext() 
        => new DataDbContext(Options);
}