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
            var posts = app.MapGroup("api/posts");

            posts.MapGet("/{id}", GetPostById)
                .WithName("GetPostById");

            posts.MapPost("/", CreatePost)
                .WithName("CreatePost");

            posts.MapGet("/getAll", GetAllPosts)
                .WithName("GetAllPosts");

            posts.MapPut("/{id}", UpdatePost)
                .WithName("UpdatePost");

            posts.MapDelete("/{id}", DeletePost)
                .WithName("DeletePost");
        }


        private async Task<IResult> GetPostById(IMediator mediator, int id)
        {
            var getPost = new GetPostByIdCommand { Id = id };
            var post = await mediator.Send(getPost);
            if (post is null) return Results.NotFound();
            return TypedResults.Ok(post);
        }

        private async Task<IResult> CreatePost(IMediator mediator, Post command)
        {
            var createPost = new CreatePostCommand { Content = command.Content };
            var post = await mediator.Send(createPost);
            return Results.CreatedAtRoute("GetPostById", new { id = post.Id }, post);
        }

        private async Task<IResult> GetAllPosts(IMediator mediator)
        {
            var getAllPosts = new GetAllPostsCommand();
            var posts = await mediator.Send(getAllPosts);
            return TypedResults.Ok(posts);
        }

        private async Task<IResult> UpdatePost(IMediator mediator, Post post, int id)
        {
            var updatePost = new UpdatePostCommand { Id = id, Content = post.Content };
            var newPost = await mediator.Send(updatePost);
            return TypedResults.Ok(newPost);
        }

        private async Task<IResult> DeletePost(IMediator mediator, int id)
        {
            var deletePost = new DeletePostCommand { Id = id };
            await mediator.Send(deletePost);
            return TypedResults.NoContent();
        }
    }
}
