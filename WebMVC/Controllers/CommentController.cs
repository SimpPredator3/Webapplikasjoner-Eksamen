using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using WebMVC.DAL;
using WebMVC.Models;
using Microsoft.Extensions.Logging;

namespace WebMVC.Controllers
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

        // DELETE: Comment/Delete/{id}
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                _logger.LogError("[CommentController] Comment not found for the CommentId {CommentId}", id);
                return NotFound("Comment not found.");
            }

            // Only allow the owner or an admin to delete the comment
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

            return Ok("Comment deleted successfully.");
        }

        // PUT: Comment/Edit/{id}
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [FromBody] CommentEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                _logger.LogError("[CommentController] Comment not found for the CommentId {CommentId}", id);
                return NotFound("Comment not found.");
            }

            // Only allow the owner to edit the comment
            if (comment.Author != User.Identity.Name)
            {
                _logger.LogWarning("[CommentController] Unauthorized edit attempt by user {User} for CommentId {CommentId}", User.Identity.Name, id);
                return Forbid();
            }

            // Update comment content and last modified date
            comment.Content = model.Content;
            comment.LastModifiedDate = DateTime.Now;

            bool success = await _commentRepository.UpdateCommentAsync(comment);
            if (!success)
            {
                _logger.LogError("[CommentController] Comment update failed for CommentId {CommentId}", id);
                return BadRequest("Comment update failed.");
            }

            return Ok("Comment updated successfully.");
        }
    }

