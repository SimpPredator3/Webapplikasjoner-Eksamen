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
