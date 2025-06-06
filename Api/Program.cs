using Application.Services;
using Application.UseCases.Authors;
using Application.UseCases.Blogs;
using Data;
using Data.Repositories;
using Domain.Authors;
using Domain.Blogs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<DataDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateBlogCommand).Assembly));

// Repositories
builder.Services.AddScoped<IBlogRepository<BlogId, Blog>, BlogRepository>();
builder.Services.AddScoped<IAuthorRepository<AuthorId, Author>, AuthorRepository>();
// Domain Services
builder.Services.AddScoped<IBlogDomainService, BlogDomainService>();
builder.Services.AddScoped<IAuthorDomainService, AuthorDomainService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();