using System.Threading.Tasks;
using api.Models;

namespace api.DAL
{
    public interface IUpvoteRepository
    {
        Task<int> GetUpvotesCountByPostIdAsync(int postId);
        Task<Upvote?> GetUpvoteAsync(int postId, string userId);
        Task AddUpvoteAsync(Upvote upvote);
        Task RemoveUpvoteAsync(Upvote upvote);
    }
}