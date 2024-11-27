using api.Models;

namespace api.DAL
{
    // Interface defining the contract for comment-related data operations
    public interface ICommentRepository
    {
        // Retrieves all comments associated with a specific post ID
        Task<List<Comment>> GetCommentsByPostIdAsync(int postId);

        // Retrieves a single comment by its unique ID
        Task<Comment?> GetCommentByIdAsync(int id);

        // Adds a new comment to the data source
        Task<bool> AddCommentAsync(Comment comment);

        // Deletes a comment from the data source by its ID
        Task<bool> DeleteCommentAsync(int id);

        // Updates an existing comment in the data source
        Task<bool> UpdateCommentAsync(Comment comment);
    }
}
