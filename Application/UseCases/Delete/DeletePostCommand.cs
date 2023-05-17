using Domain.Models;
using MediatR;

namespace Application.UseCases.Delete
{
    public class DeletePostCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
