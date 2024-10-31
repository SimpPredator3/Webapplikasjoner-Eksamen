using Microsoft.AspNetCore.Mvc;
using api.DAL;
using api.Models;
using api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace api.Controllers
{
    public class AdminDashController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<AdminDashController> _logger;

        // Constructor that injects the repository
        public AdminDashController(IPostRepository postRepository, ILogger<AdminDashController> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }

        // GET: AdminDash/Index
        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetAllPostsWithCommentCountAsync();
            if (posts == null)
            {
                _logger.LogError("[AdminDashController] Post list not found while executing _postRepository.GetAllPostsAsync()");
                return NotFound("Post list not found");
            }
            return View("~/Views/AdminDash/Index.cshtml", posts); // Updated view path for AdminDash
        }

        // GET: AdminDash/Create
        [Authorize]
        public IActionResult Create()
        {
            return View(new PostCreateViewModel());
        }

        // POST: AdminDash/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
                {
                    return Forbid();
                }

                var post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    ImageUrl = model.ImageUrl,
                    Author = User.Identity.Name,
                    Tag = model.Tag,
                    CreatedDate = DateTime.Now
                };

                bool returnOK = await _postRepository.AddPostAsync(post);
                if (returnOK)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            _logger.LogWarning("[AdminDashController] Post creation failed {@post}", model);
            return View(model);
        }

        // GET: AdminDash/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                _logger.LogError("[AdminDashController] Post not found for the PostId {PostId:0000}", id);
                return NotFound("Post not found for the PostId");
            }
            return View(post);
        }

        // GET: AdminDash/Edit/{id}
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogError("[AdminDashController] Edit action called with a null PostId.");
                return NotFound("PostId cannot be null.");
            }

            var post = await _postRepository.GetPostByIdAsync(id.Value);

            if (post == null)
            {
                _logger.LogError("[AdminDashController] Post not found when updating the PostId {PostId:0000}", id);
                return BadRequest("Post not found for the PostId");
            }

            if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
            {
                return Forbid();
            }

            if (post.Author != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(post);
        }

        // POST: AdminDash/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
            {
                return Forbid();
            }

            if (post.Author != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                bool returnOK = await _postRepository.UpdatePostAsync(post);
                if (returnOK)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            _logger.LogWarning("[AdminDashController] post update failed {@post}", post);
            return View(post);
        }

        // GET: AdminDash/Delete/{id}
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetPostByIdAsync(id.Value);
            if (post == null)
            {
                _logger.LogError("[AdminDashController] Post not found for the PostId {PostId:0000}", id);
                return BadRequest("Post not found for the PostId");
            }

            if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
            {
                return Forbid();
            }

            if (post.Author != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(post);
        }

        // POST: AdminDash/DeleteConfirmed/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                _logger.LogError("[AdminDashController] Post deletion failed for the PostId {PostId:0000}", id);
                return BadRequest("Post deletion failed");
            }

            if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
            {
                return Forbid();
            }

            if (post.Author == null || (post.Author != User.Identity.Name && !User.IsInRole("Admin")))
            {
                return Forbid();
            }

            await _postRepository.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
