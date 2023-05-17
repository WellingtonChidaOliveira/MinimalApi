using Application.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.UseCases.GetPostById
{
    public class GetPostByIdCommandHandler : IRequestHandler<GetPostByIdCommand, Post>
    {
        private readonly IPostRepository _postRepository;

        public GetPostByIdCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Post> Handle(GetPostByIdCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetPostById(request.Id);
            return post;
        }
    }
}
