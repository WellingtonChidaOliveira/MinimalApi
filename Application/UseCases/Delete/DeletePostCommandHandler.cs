using Application.Abstractions;
using MediatR;

namespace Application.UseCases.Delete
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, string>
    {
        private readonly IPostRepository _postRepository;

        public DeletePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<string> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            await _postRepository.DeletePost(request.Id);
            return "Post deleted successfully";
        }
    }
}
