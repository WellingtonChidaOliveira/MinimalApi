using Application.Abstractions;
using Application.UseCases.Delete;
using Application.UseCases.GetAllPosts;
using Application.UseCases.GetPostById;
using Application.UseCases.Posts;
using Application.UseCases.UpdatePost;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SocialDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddMediatR(typeof(CreatePostCommand));
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/api/posts/{id}", async (IMediator mediator, int id) =>
{
    var getPost = new GetPostByIdCommand { Id = id };
    var post = await mediator.Send(getPost);
    if (post is null) return Results.NotFound();
    return Results.Ok(post);
})
    .WithName("GetPostById");


app.MapPost("/api/posts", async (IMediator mediator, Post command) =>
{
    var createPost = new CreatePostCommand { Content = command.Content };
    var post = await mediator.Send(createPost);
    return Results.CreatedAtRoute("GetPostById", new { id = post.Id }, post);
})
    .WithName("CreatePost");

app.MapGet("/api/posts/getAll", async (IMediator mediator) =>
{
    var getAllPosts = new GetAllPostsCommand();
    var posts = await mediator.Send(getAllPosts);
    return Results.Ok(posts);

})
    .WithName("GetAllPosts");

app.MapPut("/api/posts/{id}", async (IMediator mediator, Post post, int id) =>
{
    var updatePost = new UpdatePostCommand { Id = id, Content = post.Content };
    var newPost = await mediator.Send(updatePost);
    return Results.Ok(newPost);
})
    .WithName("UpdatePost");

app.MapDelete("/api/posts/{id}", async (IMediator mediator, int id) =>
{
    var deletePost = new DeletePostCommand { Id = id };
    await mediator.Send(deletePost);
    return Results.NoContent();
})
    .WithName("DeletePost");


app.Run();


