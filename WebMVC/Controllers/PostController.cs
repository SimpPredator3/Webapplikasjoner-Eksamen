using Microsoft.AspNetCore.Mvc;
using WebMVC.DAL;
using WebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace WebMVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;

        // Constructor that injects the repository
        public PostController(IPostRepository postRepository)
        {
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
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // Ensure only logged-in users can post
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                // Check if the user is authenticated and User.Identity.Name is not null
                if (User?.Identity?.IsAuthenticated == true && !string.IsNullOrEmpty(User.Identity.Name))
                {
                    post.Author = User.Identity.Name;
                }
                else
                {
                    // Handle the case where the user is not authenticated for some reason
                    ModelState.AddModelError("", "Unable to determine the author of the post.");
                    return View(post);
                }

                post.CreatedDate = System.DateTime.Now;
                await _postRepository.AddPostAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
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
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Post/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                await _postRepository.UpdatePostAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Post/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Post/DeleteConfirmed/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _postRepository.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
