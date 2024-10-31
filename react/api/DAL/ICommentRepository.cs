using api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.DAL
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<bool> AddCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int id);
    }
}
