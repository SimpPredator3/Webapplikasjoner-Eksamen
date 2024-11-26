using Microsoft.AspNetCore.Mvc;
using api.DAL;
using api.Models;
using api.ViewModels;
using api.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace api.Controllers
{
    [ApiController]
    [Route("api/post")]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostRepository postRepository, ILogger<PostController> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPostsWithCommentCountAsync();
            if (posts == null)
            {
                _logger.LogError("[PostController] Post list not found while executing GetAllPosts()");
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


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PostDto postDto)
        {
            if (postDto == null)
            {
                return BadRequest("Post cannot be null");
            }
            var newPost = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                Author = postDto.Author,
                ImageUrl = postDto.ImageUrl,
                Tag = postDto.Tag
            };
            bool returnOk = await _postRepository.AddPostAsync(newPost);
            if (returnOk)
                return CreatedAtAction(nameof(GetAllPosts), new { id = newPost.Id }, newPost);

            _logger.LogWarning("[PostAPIController] Post creation failed {@post}", newPost);
            return StatusCode(500, "Internal server error");
        }


        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                _logger.LogError("[PostController] Post not found for the PostId {PostId}", id);
                return NotFound("Post not found");
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

        // POST: api/Post
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateViewModel model)
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

            bool result = await _postRepository.AddPostAsync(post);
            if (!result)
            {
                _logger.LogWarning("[PostController] Post creation failed {@post}", model);
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
                Upvotes = post.Upvotes.Count
            };

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, postDto);
        }

        // PUT: api/post/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            // Update the post details
            post.Title = model.Title;
            post.Content = model.Content;
            post.ImageUrl = model.ImageUrl;
            post.Tag = model.Tag;

            bool result = await _postRepository.UpdatePostAsync(post);
            if (!result)
            {
                return StatusCode(500, "A problem happened while updating the post.");
            }

            return Ok(model);
        }

        // DELETE: api/post/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            bool result = await _postRepository.DeletePostAsync(id);
            if (!result)
            {
                return StatusCode(500, "A problem happened while deleting the post.");
            }

            return NoContent();
        }
    }
}