using Microsoft.EntityFrameworkCore;
using WebMVC.Models;


namespace WebMVC.DAL;
public class LikesRepository : ILikesRepository
{
    private readonly ApplicationDbContext _context;

    public LikesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get if a user has already liked a post
    public async Task<Likes?> GetLikesAsync(int postId, string userId)
    {
        return await _context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
    }

    // Add a new like
    public async Task AddLikeAsync(Likes like)
    {
        _context.Likes.Add(like);
        await _context.SaveChangesAsync();
    }

    // Remove a like
    public async Task RemoveLikeAsync(Likes like)
    {
        _context.Likes.Remove(like);
        await _context.SaveChangesAsync();
    }
}