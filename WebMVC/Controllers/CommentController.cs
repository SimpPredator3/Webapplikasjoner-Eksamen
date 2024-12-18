using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using WebMVC.DAL;
using WebMVC.Models;
using WebMVC.ViewModels;
using Microsoft.Extensions.Logging;

namespace WebMVC.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentRepository commentRepository, ILogger<CommentController> logger)
        {
            _commentRepository = commentRepository;
            _logger = logger;
        }

        // POST: Comment/Create
        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateViewModel model)
        {
            // Check if the user is authenticated and has a valid identity name
            if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
            {
                return Forbid(); // Return a 403 Forbidden response if the user is not authenticated
            }

            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Text = model.Text,
                    Author = User.Identity.Name, // Get the author's name from the logged-in user
                    CreatedDate = DateTime.Now,
                    PostId = model.PostId
                };

                bool success = await _commentRepository.AddCommentAsync(comment);
                if (success)
                {
                    return RedirectToAction("Details", "Post", new { id = model.PostId });
                }
            }

            _logger.LogWarning("[CommentController] Comment creation failed {@comment}", model);
            return BadRequest("Failed to create comment.");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                _logger.LogError("[CommentController] Comment not found for CommentId {CommentId}", id);
                return NotFound("Comment not found.");
            }

            // Check if User.Identity and User.Identity.Name are not null
            if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
            {
                return Forbid(); // Return a 403 Forbidden response if the user is not authenticated
            }

            if (comment.Author != User.Identity.Name && !User.IsInRole("Admin"))
            {
                _logger.LogWarning("[CommentController] Unauthorized delete attempt by user {User} for CommentId {CommentId}", User.Identity.Name, id);
                return Forbid();
            }

            bool success = await _commentRepository.DeleteCommentAsync(id);
            if (!success)
            {
                _logger.LogError("[CommentController] Comment deletion failed for CommentId {CommentId}", id);
                return BadRequest("Comment deletion failed.");
            }

            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] CommentEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                _logger.LogError("[CommentController] Comment not found for CommentId {CommentId}", id);
                return NotFound("Comment not found.");
            }

            // Check if User.Identity and User.Identity.Name are not null
            if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
            {
                return Forbid(); // Return a 403 Forbidden response if the user is not authenticated
            }

            if (comment.Author != User.Identity.Name && !User.IsInRole("Admin"))
            {
                _logger.LogWarning("[CommentController] Unauthorized edit attempt by user {User} for CommentId {CommentId}", User.Identity.Name, id);
                return Forbid();
            }

            comment.Text = model.Text;
            comment.LastModifiedDate = DateTime.Now;

            bool success = await _commentRepository.UpdateCommentAsync(comment);
            if (!success)
            {
                _logger.LogError("[CommentController] Comment update failed for CommentId {CommentId}", id);
                return BadRequest("Comment update failed.");
            }

            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }
    }
}