using WebMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebMVC.DAL
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<bool> AddCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int id);
        Task<bool> UpdateCommentAsync(Comment comment);
    }
}
