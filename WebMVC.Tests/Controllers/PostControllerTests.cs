using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using WebMVC.Controllers;
using WebMVC.Models;
using WebMVC.DAL;
using WebMVC.ViewModels;


namespace WebMVC.Test.Controllers;

public class PostControllerTests
{

        private Mock<IPostRepository> _mockPostRepository;
        private Mock<ILogger<PostController>> _mockLogger;
        private PostController _postController;

        public PostControllerTests()
        {
            _mockPostRepository = new Mock<IPostRepository>();
            _mockLogger = new Mock<ILogger<PostController>>();
            _postController = new PostController(_mockPostRepository.Object, _mockLogger.Object);
        }

    [Fact]
    public async Task TestIndex()
    {
        // Arrange
        var postList = new List<Post>()
        {
            new Post 
            { 
                Id = 1, 
                Title = "Post 1", 
                Content = "Content 1",
                Author = "John Doe",
                ImageUrl = "https://miro.medium.com/v2/resize:fit:1200/1*uNCVd_VqFOcdxhsL71cT5Q.jpeg",
                Tag = "Technology"
            },
            new Post
            {
                Id = 2, 
                Title = "Post 2", 
                Content = "Content 2",
                Author = "Jane Smith",
                ImageUrl = "https://t3.ftcdn.net/jpg/07/46/74/96/360_F_746749607_zDV4D3BHULyb1NVvRVWujOSwNJWv8ufK.jpg",
                Tag = "Health"
            }
        };

        var mockPostRepository = new Mock<IPostRepository>();
        mockPostRepository.Setup(repo => repo.GetAllPostsAsync()).ReturnsAsync(postList);
        var mockLogger = new Mock<ILogger<PostController>>();
        var postController = new PostController(mockPostRepository.Object, mockLogger.Object);

        // Act
        var result = await postController.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Post>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count());
        Assert.Equal(postList, model);
    }

    [Fact]
    public async Task TestCreateNotOk()
    {
        // Arrange
        var testPostViewModel = new PostCreateViewModel
        {
            Title = "Post 1",
            Content = "Content 1",
            ImageUrl = "https://miro.medium.com/v2/resize:fit:1200/1*uNCVd_VqFOcdxhsL71cT5Q.jpeg"
        };

        var mockPostRepository = new Mock<IPostRepository>();
        mockPostRepository.Setup(repo => repo.AddPostAsync(It.IsAny<Post>())).ReturnsAsync(false);
        var mockLogger = new Mock<ILogger<PostController>>();
        var postController = new PostController(mockPostRepository.Object, mockLogger.Object);


        // Mocking the user context
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "Test User"), // Mock a logged-in user
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = principal };
        postController.ControllerContext.HttpContext = httpContext;


        // Act
        var result = await postController.Create(testPostViewModel);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<PostCreateViewModel>(viewResult.ViewData.Model);
        Assert.Equal(testPostViewModel.Title, model.Title);
        Assert.Equal(testPostViewModel.Content, model.Content);
        Assert.Equal(testPostViewModel.ImageUrl, model.ImageUrl);
    }

     [Fact]
        public async Task TestDetails_PostFound()
        {
            // Arrange
            var post = new Post { Id = 1, Title = "Post 1", Content = "Content 1", Author = "John Doe" };
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(1)).ReturnsAsync(post);

            // Act
            var result = await _postController.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Post>(viewResult.ViewData.Model);
            Assert.Equal(post.Id, model.Id);
        }
        [Fact]
        public async Task TestDetails_PostNotFound()
        {
            // Arrange
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(1)).ReturnsAsync((Post)null);

            // Act
            var result = await _postController.Details(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Post not found for the PostId", notFoundResult.Value);
        }

        [Fact]
        public async Task TestEdit_PostFound_Authorized()
        {
            // Arrange
            var post = new Post { Id = 1, Title = "Post 1", Content = "Content 1", Author = "Test User" };
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(1)).ReturnsAsync(post);
            SetUser("Test User");

            // Act
            var result = await _postController.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Post>(viewResult.ViewData.Model);
            Assert.Equal(post.Id, model.Id);
        }

        [Fact]
        public async Task TestEdit_PostNotFound()
        {
            // Arrange
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(1)).ReturnsAsync((Post)null);

            // Act
            var result = await _postController.Edit(1);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Post not found for the PostId", badRequestResult.Value);
        }

        [Fact]
        public async Task TestEdit_PostNotAuthorized()
        {
            // Arrange
            var post = new Post { Id = 1, Title = "Post 1", Content = "Content 1", Author = "Other User" };
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(1)).ReturnsAsync(post);
            SetUser("Test User");

            // Act
            var result = await _postController.Edit(1);

            // Assert
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task TestDelete_PostFound_Authorized()
        {
            // Arrange
            var post = new Post { Id = 1, Title = "Post 1", Content = "Content 1", Author = "Test User" };
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(1)).ReturnsAsync(post);
            SetUser("Test User");

            // Act
            var result = await _postController.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Post>(viewResult.ViewData.Model);
            Assert.Equal(post.Id, model.Id);
        }

        [Fact]
        public async Task TestDelete_PostNotFound()
        {
            // Arrange
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(1)).ReturnsAsync((Post)null);

            // Act
            var result = await _postController.Delete(1);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("IPost not found for the PostId", badRequestResult.Value);
        }

        [Fact]
        public async Task TestDelete_PostNotAuthorized()
        {
            // Arrange
            var post = new Post { Id = 1, Title = "Post 1", Content = "Content 1", Author = "Other User" };
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(1)).ReturnsAsync(post);
            SetUser("Test User");

            // Act
            var result = await _postController.Delete(1);

            // Assert
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task TestDeleteConfirmed_PostFound_Authorized()
        {
            // Arrange
            var post = new Post { Id = 1, Title = "Post 1", Content = "Content 1", Author = "Test User" };
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(1)).ReturnsAsync(post);
            SetUser("Test User");

            // Act
            var result = await _postController.DeleteConfirmed(1);

            // Assert
            _mockPostRepository.Verify(repo => repo.DeletePostAsync(1), Times.Once);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task TestDeleteConfirmed_PostNotFound()
        {
            // Arrange
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(1)).ReturnsAsync((Post)null);

            // Act
            var result = await _postController.DeleteConfirmed(1);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Post deletion failed", badRequestResult.Value);
        }

        [Fact]
        public async Task TestDeleteConfirmed_PostNotAuthorized()
        {
            // Arrange
            var post = new Post { Id = 1, Title = "Post 1", Content = "Content 1", Author = "Other User" };
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(1)).ReturnsAsync(post);
            SetUser("Test User");

            // Act
            var result = await _postController.DeleteConfirmed(1);

            // Assert
            Assert.IsType<ForbidResult>(result);
        }

        private void SetUser(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);
            _postController.ControllerContext.HttpContext = new DefaultHttpContext { User = principal };
        }
}
