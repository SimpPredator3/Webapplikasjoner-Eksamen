using Microsoft.AspNetCore.Mvc;
using api.DAL;
using api.Models;
using api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace api.Controllers
{
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
        public async Task<IActionResult> Index(int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                _logger.LogError("[CommentController] Post not found for PostId {PostId}", postId);
                return NotFound("Post not found");
            }

            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
            return View(comments);
        }

        // POST: Add a new comment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(int postId, CommentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = await _postRepository.GetPostByIdAsync(postId);
                if (post == null)
                {
                    _logger.LogError("[CommentController] Post not found for PostId {PostId}", postId);
                    return NotFound("Post not found");
                }

                // Check if User.Identity and User.Identity.Name are not null
                if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
                {
                    return Forbid(); // Return a 403 Forbidden response if the user is not authenticated
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
                    return RedirectToAction("Details", "Post", new { id = postId });
                }
            }

            _logger.LogWarning("[CommentController] Comment creation failed {@model}", model);
            return View(model);
        }
    }
}