using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.DAL
{
    public class UpvoteRepository : IUpvoteRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UpvoteRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetUpvotesCountByPostIdAsync(int postId)
        {
            return await _dbContext.Upvotes.CountAsync(upvote => upvote.PostId == postId);
        }

        public async Task<Upvote?> GetUpvoteAsync(int postId, string userId)
        {
            return await _dbContext.Upvotes
                .FirstOrDefaultAsync(upvote => upvote.PostId == postId && upvote.UserId == userId);
        }

        public async Task AddUpvoteAsync(Upvote upvote)
        {
            await _dbContext.Upvotes.AddAsync(upvote);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveUpvoteAsync(Upvote upvote)
        {
            _dbContext.Upvotes.Remove(upvote);
            await _dbContext.SaveChangesAsync();
        }
    }
}