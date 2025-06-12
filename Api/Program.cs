using System.Text.Json.Serialization;
using Api.Middleware;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Application.UseCases.Authors;
using Application.UseCases.Blogs;
using Data;
using Data.Repositories;
using Domain.Authors;
using Domain.Blogs;
using Domain.Posts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>(); // Register the global exception handling middleware
builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<DataDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateBlog).Assembly));
builder.Services.AddAutoMapper(typeof(DomainToDtoProfile));

// Repositories
builder.Services.AddScoped<IBlogRepository<BlogId, Blog>, BlogRepository>();
builder.Services.AddScoped<IAuthorRepository<AuthorId, Author>, AuthorRepository>();
builder.Services.AddScoped<IPostRepository<PostId, Post>, PostRepository>();
// Domain Services
builder.Services.AddScoped<IBlogValidateService, BlogValidateService>();
builder.Services.AddScoped<IAuthorValidateService, AuthorValidateService>();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();