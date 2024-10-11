using Microsoft.AspNetCore.Mvc;
using WebMVC.DAL;
using WebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class MainController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<MainController> _logger;
        public MainController(IPostRepository postRepository, ILogger<MainController> logger)
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
    }
}
