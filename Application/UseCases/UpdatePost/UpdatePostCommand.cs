using Domain.Models;
using MediatR;

namespace Application.UseCases.UpdatePost
{
    public class UpdatePostCommand : IRequest<Post>
    {
        public int Id { get; set; }
        public string? Content { get; set; }
    }
}
