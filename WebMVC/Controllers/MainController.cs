using Microsoft.AspNetCore.Mvc;
using WebMVC.DAL;
using WebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using WebMVC.ViewModels;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

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
                _logger.LogError("[MainController] Post list not found while executing _postRepository.GetAllPostsAsync()");
                return NotFound("Post list not found");
            }

            // Initialize tag-related data structures
            var uniqueTags = new Dictionary<string, int>(); // Dictionary to store tags and their counts
            int noTags = 0; // Count for posts with no tags
            int allTags = 0; // Total number of posts

            // Process the posts and count tags
            foreach (var post in posts)
            {
                allTags++; // Increment total posts count

                if (string.IsNullOrEmpty(post.Tag))
                {
                    noTags++; // Increment no tags count
                }
                else
                {
                    if (uniqueTags.ContainsKey(post.Tag))
                    {
                        uniqueTags[post.Tag]++; // Increment existing tag count
                    }
                    else
                    {
                        uniqueTags.Add(post.Tag, 1); // Add new tag with count 1
                    }
                }
            }

            // Pass data to the view using ViewBag
            ViewBag.NoTags = noTags;
            ViewBag.AllTags = allTags;
            ViewBag.UniqueTags = uniqueTags;

            // Pass posts to the view
            return View(posts);
        }
    }
}
