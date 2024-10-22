using Microsoft.AspNetCore.Mvc;
using WebMVC.DAL;
using WebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using WebMVC.ViewModels;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace WebMVC.Controllers
{
    public class MainController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ILikesRepository _likesRepository;  // Add ILikesRepository
        private readonly ILogger<MainController> _logger;

        // Update the constructor to inject ILikesRepository
        public MainController(IPostRepository postRepository, ILikesRepository likesRepository, ILogger<MainController> logger)
        {
            _postRepository = postRepository;
            _likesRepository = likesRepository;  // Assign it to a field
            _logger = logger;
        }

        // GET: Post/Index
        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetAllPostsWithCommentCountAsync();
            if (posts == null)
            {
                // Log an error if the posts list is not found and return a NotFound response.
                _logger.LogError("[MainController] Post list not found while executing _postRepository.GetAllPostsAsync()");
                return NotFound("Post list not found");
            }

            // Initialize tag-related data structures
            var uniqueTags = new Dictionary<string, int>(); // Dictionary to store tags and their counts
            int noTags = 0; // Count for posts with no tags
            int allTags = 0; // Total number of posts

            // Process the posts and count tags
            foreach (var post in posts)
            {
                allTags++; // Increment total posts count

                if (string.IsNullOrEmpty(post.Tag))
                {
                    noTags++; // Increment no tags count
                }
                else
                {
                    if (uniqueTags.ContainsKey(post.Tag))
                    {
                        uniqueTags[post.Tag]++; // Increment existing tag count
                    }
                    else
                    {
                        uniqueTags.Add(post.Tag, 1); // Add new tag with count 1
                    }
                }
            }

            // Pass data to the view using ViewBag
            ViewBag.NoTags = noTags;
            ViewBag.AllTags = allTags;
            ViewBag.UniqueTags = uniqueTags;

            // Pass posts to the view
            return View(posts);
        }

        // Like Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Upvote(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in user's ID
            if (userId == null)
            {
                return Unauthorized(); // User must be logged in to vote
            }

            // Check if the user has already liked the post
            var existingLike = await _likesRepository.GetLikesAsync(postId, userId);
            if (existingLike != null)
            {
                // If the user has already liked the post, revoke the like
                return await RevokeLike(postId); // Call the RevokeLike method
            }

            // Record the upvote
            var like = new Likes
            {
                IsLike = true,
                PostId = postId,
                UserId = userId
            };
            await _likesRepository.AddLikeAsync(like);

            // Safely update the post's upvote count
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound("Post not found.");
            }

            post.Upvotes++;  // Now it's safe to increment
            await _postRepository.UpdatePostAsync(post);

            // Return JSON with the updated upvote count
            return Json(new { success = true, upvotes = post.Upvotes });
        }

        //Revoke Like
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> RevokeLike(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in user's ID
            if (userId == null)
            {
                return Unauthorized(); // User must be logged in to revoke like
            }

            // Check if the user has already liked the post
            var existingLike = await _likesRepository.GetLikesAsync(postId, userId);
            if (existingLike == null)
            {
                return BadRequest("You haven't liked this post yet.");
            }

            // Remove the like
            await _likesRepository.RemoveLikeAsync(existingLike);

            // Safely update the post's upvote count
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound("Post not found.");
            }

            post.Upvotes--;  // Now it's safe to decrement
            await _postRepository.UpdatePostAsync(post);

            // Return JSON with the updated upvote count
            return Json(new { success = true, upvotes = post.Upvotes });
        }
    }
}
