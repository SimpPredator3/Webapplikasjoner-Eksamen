using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.DAL;
using api.Models;
using System.Security.Claims;

namespace api.Controllers
{
    [ApiController]
    [Route("api/upvote")]
    public class UpvoteController : ControllerBase
    {
        private readonly IUpvoteRepository _upvoteRepository;
        private readonly IPostRepository _postRepository;

        public UpvoteController(IUpvoteRepository upvoteRepository, IPostRepository postRepository)
        {
            _upvoteRepository = upvoteRepository;
            _postRepository = postRepository;
        }

        [HttpPost("{postId}")]
        [Authorize] // Ensure user authentication
        public async Task<IActionResult> ToggleUpvote(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User must be logged in to upvote." });
            }

            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound(new { message = "Post not found." });
            }

            // Check if user already upvoted the post
            var existingUpvote = await _upvoteRepository.GetUpvoteAsync(postId, userId);
            if (existingUpvote != null)
            {
                // If the user already upvoted, remove the upvote
                await _upvoteRepository.RemoveUpvoteAsync(existingUpvote);
                post.UpvoteCount--; // Decrement upvote count
            }
            else
            {
                // Otherwise, add a new upvote
                var upvote = new Upvote
                {
                    PostId = postId,
                    UserId = userId,
                };

                await _upvoteRepository.AddUpvoteAsync(upvote);
                post.UpvoteCount++; // Increment upvote count
            }

            // Update the post in the database
            await _postRepository.UpdatePostAsync(post);

            // Return the updated upvote count
            return Ok(new { upvotes = post.UpvoteCount });
        }
    }
}