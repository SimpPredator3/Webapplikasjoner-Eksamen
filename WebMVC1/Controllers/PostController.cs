using Microsoft.AspNetCore.Mvc;
using WebMVC1.DAL;
using WebMVC1.Models;
using System.Threading.Tasks;


namespace WebMVC1.Controllers
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
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
