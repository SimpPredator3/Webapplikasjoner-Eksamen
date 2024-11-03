using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC.DAL
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get a single comment by ID
        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        // Get all comments for a specific post
        public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }

        // Add a new comment
        public async Task<bool> AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            return await _context.SaveChangesAsync() > 0;
        }

        // Update an existing comment
        public async Task<bool> UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            return await _context.SaveChangesAsync() > 0;
        }

        // Delete a comment, checking that the author matches or user is an admin
        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await GetCommentByIdAsync(id);
            if (comment == null) return false;

            _context.Comments.Remove(comment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
