using Application.Abstractions;
using Application.UseCases.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Persistence;

namespace MinimalApi.Extensions
{
    public static class MinimalApiExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SocialDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddMediatR(typeof(CreatePostCommand));
        }
    }
}
