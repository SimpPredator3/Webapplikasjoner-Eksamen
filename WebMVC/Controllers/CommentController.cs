using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using WebMVC.DAL;
using WebMVC.Models;
using WebMVC.ViewModels;
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

        // POST: Comment/Create
        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Content = model.Content,
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

        // DELETE: Comment/Delete/{id}
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                _logger.LogError("[CommentController] Comment not found for CommentId {CommentId}", id);
                return NotFound("Comment not found.");
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

            return Ok("Comment deleted successfully.");
        }

        // PUT: Comment/Edit/{id}
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] CommentEditViewModel model)
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

            if (comment.Author != User.Identity.Name)
            {
                _logger.LogWarning("[CommentController] Unauthorized edit attempt by user {User} for CommentId {CommentId}", User.Identity.Name, id);
                return Forbid();
            }

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
