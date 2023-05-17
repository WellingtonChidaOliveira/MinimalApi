using Domain.Models;
using MediatR;

namespace Application.UseCases.GetPostById
{
    public class GetPostByIdCommand : IRequest<Post>
    {
        public int Id { get; set; }
    }
}
