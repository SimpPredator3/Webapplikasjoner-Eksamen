using Microsoft.AspNetCore.Mvc;
using api.DAL;
using api.Models;
using api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using api.DTOs;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly ILogger<CommentController> _logger;

        // Constructor to inject required dependencies
        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, ILogger<CommentController> logger)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _logger = logger;
        }

        // GET: Retrieve all comments for a specific post
        [HttpGet("{postId}")]
        public async Task<IActionResult> Index(int postId)
        {
            Console.WriteLine($"PostId: {postId}"); // Log postId for debugging
            var post = await _postRepository.GetPostByIdAsync(postId); // Check if the post exists
            if (post == null)
            {
                _logger.LogError("[CommentController] Post not found for PostId {PostId}", postId); // Log error
                return NotFound("Post not found"); // Return 404 Not Found
            }

            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId); // Retrieve comments for the post
            return Ok(comments); // Return 200 OK with comments
        }

        // POST: Add a new comment to a specific post
        [HttpPost("{postId}")]
        [Authorize] // Require user authentication
        public async Task<IActionResult> Create(int postId, [FromBody] CommentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("[CommentController] Invalid model state for {@model}", model); // Log validation issues
                return BadRequest(ModelState); // Return 400 Bad Request if validation fails
            }

            var post = await _postRepository.GetPostByIdAsync(postId); // Check if the post exists
            if (post == null)
            {
                _logger.LogError("[CommentController] Post not found for PostId {PostId}", postId); // Log error
                return NotFound("Post not found"); // Return 404 Not Found
            }

            if (User?.Identity?.IsAuthenticated != true || string.IsNullOrEmpty(User.Identity.Name))
            {
                return Forbid(); // Return 403 Forbidden if user is not authenticated
            }

            // Create a new Comment entity from the provided data
            var comment = new Comment
            {
                Text = model.Text,
                Author = User.Identity.Name!, // Set the author to the authenticated user's name
                CreatedDate = DateTime.Now,
                PostId = postId
            };

            bool result = await _commentRepository.AddCommentAsync(comment); // Add the comment to the database
            if (result)
            {
                return CreatedAtAction(nameof(Create), new { postId }, comment); // Return 201 Created with the new comment data
            }

            _logger.LogWarning("[CommentController] Failed to add comment {@model}", model); // Log failure
            return StatusCode(500, "Failed to add comment"); // Return 500 Internal Server Error
        }

        // DELETE: Delete a comment by its ID
        [HttpDelete("{commentId}")]
        [Authorize] // Require user authentication
        public async Task<IActionResult> Delete(int commentId)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId); // Check if the comment exists
            if (comment == null)
            {
                _logger.LogError("[CommentController] Comment not found for CommentId {CommentId}", commentId); // Log error
                return NotFound("Comment not found"); // Return 404 Not Found
            }

            if (User?.Identity?.IsAuthenticated != true || string.IsNullOrEmpty(User.Identity.Name))
            {
                return Forbid(); // Return 403 Forbidden if user is not authenticated
            }

            if (comment.Author != User.Identity.Name)
            {
                return Forbid(); // Return 403 Forbidden if the user is not the author of the comment
            }

            bool result = await _commentRepository.DeleteCommentAsync(commentId); // Attempt to delete the comment
            if (result)
            {
                return NoContent(); // Return 204 No Content on successful deletion
            }

            _logger.LogWarning("[CommentController] Failed to delete comment for CommentId {CommentId}", commentId); // Log failure
            return StatusCode(500, "Failed to delete comment"); // Return 500 Internal Server Error
        }

        // PUT: Update a comment by its ID
        [HttpPut("{commentId}")]
        [Authorize] // Require user authentication
        public async Task<IActionResult> Update(int commentId, [FromBody] CommentDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("[CommentController] Invalid model state for {@model}", model); // Log validation issues
                return BadRequest(ModelState); // Return 400 Bad Request if validation fails
            }

            var comment = await _commentRepository.GetCommentByIdAsync(commentId); // Check if the comment exists
            if (comment == null)
            {
                _logger.LogError("[CommentController] Comment not found for CommentId {CommentId}", commentId); // Log error
                return NotFound("Comment not found"); // Return 404 Not Found
            }

            if (User?.Identity?.IsAuthenticated != true || string.IsNullOrEmpty(User.Identity.Name))
            {
                return Forbid(); // Return 403 Forbidden if user is not authenticated
            }

            if (comment.Author != User.Identity.Name)
            {
                return Forbid(); // Return 403 Forbidden if the user is not the author of the comment
            }

            // Update the comment's fields
            comment.Text = model.Comment;
            comment.CreatedDate = DateTime.Now; // Update the created date to now

            bool result = await _commentRepository.UpdateCommentAsync(comment); // Attempt to update the comment
            if (result)
            {
                return NoContent(); // Return 204 No Content on successful update
            }

            _logger.LogWarning("[CommentController] Failed to update comment for CommentId {CommentId}", commentId); // Log failure
            return StatusCode(500, "Failed to update comment"); // Return 500 Internal Server Error
        }
    }
}
