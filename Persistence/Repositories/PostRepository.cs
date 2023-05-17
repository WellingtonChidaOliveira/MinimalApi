using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public sealed class PostRepository : IPostRepository
    {
        private readonly SocialDbContext _context;

        public PostRepository(SocialDbContext context)
        {
            _context = context;
        }

        public async Task<Post> CreatePost(Post post)
        {
            await _context.Post.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task DeletePost(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post is null)
            {
                return;
            }
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

        }

        public async Task<ICollection<Post>> GetAllPosts()
        {
            return await _context.Post.ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Post.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Post> UpdatePost( int postId, string updateContent)
        {

            var postToUpdate = await _context.Post.FirstOrDefaultAsync(p => p.Id == postId);
            if (postToUpdate is null)
            {
                return null;
            }
            postToUpdate.Content = updateContent;
            postToUpdate.LastModified = DateTime.Now;
            await _context.SaveChangesAsync();
            return postToUpdate;

        }
    }
}
