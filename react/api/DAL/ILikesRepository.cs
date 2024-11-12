using api.Models;

namespace api.DAL
{
    public interface ILikesRepository
    {
        Task<Likes?> GetLikesAsync(int postId, string userId);  // To check if a user has liked a post
        Task AddLikeAsync(Likes like);  // To add a new like
        Task RemoveLikeAsync(Likes like);  // To remove a like
    }
}
