using Application.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.UseCases.Posts
{
    public class CreatPostCommandHandler : IRequestHandler<CreatePostCommand, Post>
    {
        private readonly IPostRepository _postRepository;

        public CreatPostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post
            {
                Content = request.Content
            };

            return await _postRepository.CreatePost(post);
        }
    }
}
