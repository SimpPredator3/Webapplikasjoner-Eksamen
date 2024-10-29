using Microsoft.EntityFrameworkCore;
using WebMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebMVC.DAL
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<PostRepository> _logger;

        public PostRepository(ApplicationDbContext context, ILogger<PostRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            try
            {
                // Attempt to fetch the list of posts from the database.
                return await _context.Posts.ToListAsync();
            }
            catch (Exception e)
            {
                // Log an error if the retrieval of posts fails and return null.
                _logger.LogError("[PostRepository] posts ToListAsync() failed when GetAllPostsAsunc(), error message: {e}", e.Message);
                return new List<Post>(); // Return an empty list instead of null
            }
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            try
            {
                // Use Include to load the related Comments when retrieving the Post
                return await _context.Posts
                    .Include(p => p.Comments)  // Include the related comments
                    .FirstOrDefaultAsync(p => p.Id == id); // Use FirstOrDefaultAsync instead of FindAsync
            }
            catch (Exception e)
            {
                // Log an error if the retrieval of the post fails and return null.
                _logger.LogError("[PostRepository] post FindAsync(id) failed when GetPostByIdAsync() for PostId {PostId:0000}, error message: {e}", id, e.Message);
                return null;
            }
        }



        public async Task<bool> AddPostAsync(Post post)
        {
            try
            {
                post.CreatedDate = DateTime.Now;
                // Attempt to add the post to the database.
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                // Log an error if the post creation fails and return false.
                _logger.LogError("[PostRepository] post creation failed for post {@post}, error message: {e}", post, e.Message);
                return false;
            }
        }




        public async Task<bool> UpdatePostAsync(Post post)
        {
            try
            {
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                // Log an error if the post update fails and return false.
                _logger.LogError("[PostRepository] post failed when trying to updating the post {@post}, error message: {e}", post, e.Message);
                return false;
            }
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);
                if (post != null)
                {
                    _context.Posts.Remove(post);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                // Log an error if the post deletion fails and return false.
                _logger.LogError("[PostRepository] post deletion failed for the PostId {PostId:0000}, error message: {e}", id, e.Message);
                return false;
            }

        }

        public async Task<IEnumerable<Post>> GetAllPostsWithCommentCountAsync()
        {
            var posts = await _context.Posts
                .Include(p => p.Comments) // Load comments with each post
                .ToListAsync();


            foreach (var post in posts)
            {
                // Make sure you're counting comments based on the correct PostId
                post.CommentCount = await _context.Comments.CountAsync(c => c.PostId == post.Id);

                // Log the post and comment count for debugging purposes
                _logger.LogInformation("Post {PostId}: {CommentCount} comments found", post.Id, post.CommentCount);
            }

            return posts;
        }
    }
}
