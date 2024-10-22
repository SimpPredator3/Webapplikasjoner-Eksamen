using Microsoft.AspNetCore.Mvc;
using WebMVC.DAL;
using WebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostController> _logger;

        // Constructor that injects the repository
        public PostController(IPostRepository postRepository, ILogger<PostController> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }

        // GET: Post/Index
        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            if (posts == null)
            {
                // Log an error if the posts list is not found and return a NotFound response.
                _logger.LogError("[PostController] Post list not found while executing _postRepository.GetAllPostsAsync()");
                return NotFound("Post list not found");
            }
            return View(posts);
        }

        // GET: Post/Create
        [Authorize] // Only logged-in users can access this method
        public IActionResult Create()
        {
            return View(new PostCreateViewModel());
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // Ensure only logged-in users can post
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if User.Identity and User.Identity.Name are not null
                if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
                {
                    return Forbid(); // Return a 403 Forbidden response if the user is not authenticated
                }

                // Map ViewModel to Post entity
                var post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    ImageUrl = model.ImageUrl,
                    Author = User.Identity.Name, // Set the Author from the logged-in user
                    CreatedDate = DateTime.Now
                };

                // Attempt to add the post to the repository.
                bool returnOK = await _postRepository.AddPostAsync(post);
                if (returnOK)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            // Log a warning if the post creation failed and return the view with the model.
            _logger.LogWarning("[PostController] Post creation failed {@post}", model);
            return View(model);
        }

        // GET: Post/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                // Log an error if the post is not found and return a NotFound response.
                _logger.LogError("[PostController] Post not found for the PostId {PostId:0000}", id);
                return NotFound("Post not found for the PostId");
            }
            return View(post);
        }

        // GET: Post/Edit/{id}
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the id is null before using id.Value
            if (id == null)
            {
                _logger.LogError("[PostController] Edit action called with a null PostId.");
                return NotFound("PostId cannot be null.");
            }

            var post = await _postRepository.GetPostByIdAsync(id.Value);

            // Check if the post was found. If not, log an error and return a BadRequest response.
            if (post == null)
            {
                _logger.LogError("[PostController] Post not found when updating the PostId {PostId:0000}", id);
                return BadRequest("Post not found for the PostId");
            }

            // Check if User.Identity and User.Identity.Name are not null
            if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
            {
                return Forbid(); // Return a 403 Forbidden response if the user is not authenticated
            }

            // Ensure the logged-in user is the author or an admin
            if (post.Author != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return Forbid(); // Return a 403 Forbidden response
            }

            return View(post); // If the user is authorized, return the view
        }

        // POST: Post/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            // Check if User.Identity and User.Identity.Name are not null
            if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
            {
                return Forbid(); // Return a 403 Forbidden response if the user is not authenticated
            }

            // Ensure the logged-in user is the author or an admin
            if (post.Author != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return Forbid(); // Return a 403 Forbidden response
            }

            if (ModelState.IsValid)
            {
                bool returnOK = await _postRepository.UpdatePostAsync(post);
                if (returnOK)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            // Log a warning if the post update failed and return the view with the model.
            _logger.LogWarning("[PostController] post update failed {@post}", post);
            return View(post);
        }

        // GET: Post/Delete/{id}
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
                // Log an error if the post is not found and return a BadRequest response.
                _logger.LogError("[PostController] Post not found for the PostId {PostId:0000}", id);
                return BadRequest("IPost not found for the PostId");
            }

            // Check if User.Identity and User.Identity.Name are not null
            if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
            {
                return Forbid(); // Return a 403 Forbidden response if the user is not authenticated
            }

            // Ensure the logged-in user is the author or an admin
            if (post.Author != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return Forbid(); // Return a 403 Forbidden response
            }

            return View(post); // If the user is authorized, return the confirmation view
        }

        // POST: Post/DeleteConfirmed/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                // Log an error if the post deletion fails and return a BadRequest response.
                _logger.LogError("[PostController] Post deletion failed for the PostId {PostId:0000}", id);
                return BadRequest("Post deletion failed");
            }

            // Check if User.Identity and User.Identity.Name are not null
            if (User?.Identity?.IsAuthenticated != true || User.Identity.Name == null)
            {
                return Forbid(); // Return a 403 Forbidden response if the user is not authenticated
            }

            // Ensure the post's Author is not null and that the logged-in user is the author or an admin
            if (post.Author == null || (post.Author != User.Identity.Name && !User.IsInRole("Admin")))
            {
                return Forbid(); // Return a 403 Forbidden response
            }

            await _postRepository.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}