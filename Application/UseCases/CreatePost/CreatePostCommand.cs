using Domain.Models;
using MediatR;

namespace Application.UseCases.Posts
{
    public class CreatePostCommand : IRequest<Post>
    {
        public string? Content { get; set; }
    }
}
