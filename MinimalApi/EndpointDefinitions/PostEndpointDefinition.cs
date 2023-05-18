using Application.UseCases.Delete;
using Application.UseCases.GetAllPosts;
using Application.UseCases.GetPostById;
using Application.UseCases.Posts;
using Application.UseCases.UpdatePost;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Abstractions;

namespace MinimalApi.EndpointDefinitions
{
    public class PostEndpointDefinition : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/api/posts/{id}", async (IMediator mediator, int id) =>
            {
                var getPost = new GetPostByIdCommand { Id = id };
                var post = await mediator.Send(getPost);
                if (post is null) return Results.NotFound();
                return Results.Ok(post);
            })
                .WithName("GetPostById");


            app.MapPost("/api/posts", async ([FromServices] IMediator mediator, Post command) =>
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

            app.MapPut("/api/posts/{id}", async ([FromServices] IMediator mediator, Post post, int id) =>
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
        }
    }
}
