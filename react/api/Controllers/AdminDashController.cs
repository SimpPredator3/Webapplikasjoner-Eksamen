using Microsoft.AspNetCore.Mvc;
using api.DAL;
using api.Models;
using api.ViewModels;
using api.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace api.Controllers
{
    [ApiController]
    [Route("api/admindash")]
    public class AdminDashController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<AdminDashController> _logger;

        public AdminDashController(IPostRepository postRepository, ILogger<AdminDashController> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }

        // GET: api/admindash
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPostsWithCommentCountAsync();
            if (posts == null)
            {
                _logger.LogError("[AdminDashController] Post list not found while executing GetAllPosts()");
                return NotFound("Post list not found");
            }

            // Inline mapping to PostDto
            var postDtos = posts.Select(post => new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                Tag = post.Tag,
                CreatedDate = post.CreatedDate,
                Author = post.Author,
                CommentCount = post.CommentCount,
                Upvotes = post.Upvotes.Count
            });

            return Ok(postDtos);
        }


        // GET: api/admindash/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                _logger.LogError("[AdminDashController] Post not found for the PostId {PostId}", id);
                return NotFound("Post not found for the PostId");
            }

            // Inline mapping to PostDto
            var postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                Tag = post.Tag,
                CreatedDate = post.CreatedDate,
                Author = post.Author,
                CommentCount = post.CommentCount,
                Upvotes = post.Upvotes.Count
            };

            return Ok(postDto);
        }


        // GET: AdminDash/Create
        [HttpPost("create")]
        [Authorize]
        public IActionResult Create()
        {
            return View(new PostCreateViewModel());
        }

        // POST: AdminDash/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] PostCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
            if (!returnOK)
            {
                _logger.LogWarning("[AdminDashController] Post creation failed {@post}", model);
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                Tag = post.Tag,
                CreatedDate = post.CreatedDate,
                Author = post.Author,
                CommentCount = post.CommentCount,
                Upvotes = 0 // Initialize upvotes to zero
            };

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, postDto);
        }


        // PUT: api/admindash/edit/{id}
        [HttpPut("edit/{id}")]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [FromBody] Post post)
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
                    return NoContent();
                }
            }

            _logger.LogWarning("[AdminDashController] Post update failed {@post}", post);
            return BadRequest(ModelState);
        }


        // DELETE: api/admindash/delete/{id}
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                _logger.LogError("[AdminDashController] Post deletion failed for the PostId {PostId:0000}", id);
                return NotFound("Post not found");
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
            return NoContent();
        }
    }
}
