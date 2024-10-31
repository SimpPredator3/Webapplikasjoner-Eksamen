using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace WebMVC.DAL
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }

        public async Task<bool> AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCommentAsync(int id, string userId)
        {
            var comment = await GetCommentByIdAsync(id);
            if (comment == null || comment.Author != userId) return false;

            _context.Comments.Remove(comment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
