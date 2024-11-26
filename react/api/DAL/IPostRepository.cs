using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.DAL
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);
        Task<bool> AddPostAsync(Post post);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(int id);
        Task<IEnumerable<Post>> GetAllPostsWithCommentCountAsync();
    }
}