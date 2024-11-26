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

        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, ILogger<CommentController> logger)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _logger = logger;
        }

        // GET: Comments for a specific post
        [HttpGet("{postId}")]
        public async Task<IActionResult> Index(int postId)
        {
            Console.WriteLine($"PostId: {postId}");
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                _logger.LogError("[CommentController] Post not found for PostId {PostId}", postId);
                return NotFound("Post not found");
            }

            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        // POST: Add a new comment
        [HttpPost("{postId}")]
        [Authorize]
        public async Task<IActionResult> Create(int postId, [FromBody] CommentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("[CommentController] Invalid model state for {@model}", model);
                return BadRequest(ModelState);
            }

            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                _logger.LogError("[CommentController] Post not found for PostId {PostId}", postId);
                return NotFound("Post not found");
            }

            if (User?.Identity?.IsAuthenticated != true || string.IsNullOrEmpty(User.Identity.Name))
            {
                return Forbid(); // Return 403 if the user is not authenticated
            }

            var comment = new Comment
            {
                Text = model.Text,
                Author = User.Identity.Name!,
                CreatedDate = DateTime.Now,
                PostId = postId
            };

            bool result = await _commentRepository.AddCommentAsync(comment);
            if (result)
            {
                return CreatedAtAction(nameof(Create), new { postId }, comment); // Return 201 Created with comment data
            }

            _logger.LogWarning("[CommentController] Failed to add comment {@model}", model);
            return StatusCode(500, "Failed to add comment");
        }
        [HttpDelete("{commentId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int commentId)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);
            if (comment == null)
            {
                _logger.LogError("[CommentController] Comment not found for CommentId {CommentId}", commentId);
                return NotFound("Comment not found");
            }

            if (User?.Identity?.IsAuthenticated != true || string.IsNullOrEmpty(User.Identity.Name))
            {
                return Forbid(); // Return 403 if the user is not authenticated
            }

            if (comment.Author != User.Identity.Name)
            {
                return Forbid(); // Return 403 if the user is not the author of the comment
            }

            bool result = await _commentRepository.DeleteCommentAsync(commentId);
            if (result)
            {
                return NoContent(); // Return 204 No Content
            }

            _logger.LogWarning("[CommentController] Failed to delete comment for CommentId {CommentId}", commentId);
            return StatusCode(500, "Failed to delete comment");
        }
        [HttpPut("{commentId}")]
        [Authorize]
        public async Task<IActionResult> Update(int commentId, [FromBody] CommentDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("[CommentController] Invalid model state for {@model}", model);
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.GetCommentByIdAsync(commentId);
            if (comment == null)
            {
                _logger.LogError("[CommentController] Comment not found for CommentId {CommentId}", commentId);
                return NotFound("Comment not found");
            }

            if (User?.Identity?.IsAuthenticated != true || string.IsNullOrEmpty(User.Identity.Name))
            {
                return Forbid(); // Return 403 if the user is not authenticated
            }

            if (comment.Author != User.Identity.Name)
            {
                return Forbid(); // Return 403 if the user is not the author of the comment
            }

            comment.Text = model.Comment;
            comment.CreatedDate = DateTime.Now;

            bool result = await _commentRepository.UpdateCommentAsync(comment);
            if (result)
            {
                return NoContent(); // Return 204 No Content
            }

            _logger.LogWarning("[CommentController] Failed to update comment for CommentId {CommentId}", commentId);
            return StatusCode(500, "Failed to update comment");
        }
    }
}