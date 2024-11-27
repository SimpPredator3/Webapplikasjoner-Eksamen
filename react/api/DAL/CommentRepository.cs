using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.DAL
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor to initialize the database context
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieves all comments associated with a specific post ID
        public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments
                                 .Where(c => c.PostId == postId) // Filter comments by the provided post ID
                                 .ToListAsync(); // Asynchronously convert the result to a list
        }

        // Retrieves a specific comment by its unique ID
        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id); // Asynchronously find a comment by its primary key
        }

        // Adds a new comment to the database
        public async Task<bool> AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment); // Add the new comment to the Comments DbSet
            return await _context.SaveChangesAsync() > 0; // Save changes and return true if at least one record is affected
        }

        // Deletes a comment from the database using its ID
        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id); // Find the comment by its primary key
            if (comment == null) return false; // Return false if the comment doesn't exist

            _context.Comments.Remove(comment); // Remove the comment from the DbSet
            return await _context.SaveChangesAsync() > 0; // Save changes and return true if deletion was successful
        }

        // Updates an existing comment in the database
        public async Task<bool> UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment); // Mark the comment entity as modified
            return await _context.SaveChangesAsync() > 0; // Save changes and return true if update was successful
        }
    }
}
