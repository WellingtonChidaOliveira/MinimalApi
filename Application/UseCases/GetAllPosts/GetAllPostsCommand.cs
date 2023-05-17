using Domain.Models;
using MediatR;

namespace Application.UseCases.GetAllPosts
{
    public class GetAllPostsCommand : IRequest<ICollection<Post>>
    {

    }
}
