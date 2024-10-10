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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPostRepository _postRepository;

        // Constructor that injects the repository
        public PostController(UserManager<IdentityUser> userManager, IPostRepository postRepository)
        {
            _userManager = userManager;
            _postRepository = postRepository;
        }

        // GET: Post/Index
        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetAllPostsAsync();
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

            // Hent den innloggede brukeren
                var user = await _userManager.GetUserAsync(User);
                post.Author = user?.Email; // Setter forfatter til brukerens email

            if (ModelState.IsValid)
            {
                // Map ViewModel to Post entity
                var post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    ImageUrl = model.ImageUrl,
                    Author = User.Identity.Name, // Set the Author from the logged-in user
                    CreatedDate = DateTime.Now
                };

                await _postRepository.AddPostAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Post/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // GET: Post/Edit/{id}
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetPostByIdAsync(id.Value);
            if (post == null)
            {
                return NotFound();
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

            // Ensure the logged-in user is the author or an admin
            if (post.Author != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return Forbid(); // Return a 403 Forbidden response
            }

            if (ModelState.IsValid)
            {
                await _postRepository.UpdatePostAsync(post);
                return RedirectToAction(nameof(Index));
            }
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
                return NotFound();
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

            // Ensure the logged-in user is the author or an admin
            if (post.Author != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return Forbid(); // Return a 403 Forbidden response
            }

            await _postRepository.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
