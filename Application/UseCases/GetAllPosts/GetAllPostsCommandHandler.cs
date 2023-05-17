using Application.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.UseCases.GetAllPosts
{
    internal class GetAllPostsCommandHandler : IRequestHandler<GetAllPostsCommand, ICollection<Post>>
    {
        private readonly IPostRepository _postRepository;

        public GetAllPostsCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<ICollection<Post>> Handle(GetAllPostsCommand request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetAllPosts();
            return posts;
        }

    }
}
