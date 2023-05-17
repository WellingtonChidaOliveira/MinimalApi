using Application.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.UseCases.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Post>
    {
        private readonly IPostRepository _postRepository;

        public UpdatePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Post> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.UpdatePost(request.Id, request.Content);
            return post;
        }
    }
}
