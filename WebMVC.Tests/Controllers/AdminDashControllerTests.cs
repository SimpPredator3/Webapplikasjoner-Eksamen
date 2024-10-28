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

public class AdminDashControllerTests
{
    private Mock<IPostRepository> _mockPostRepository;
    private Mock<ILogger<AdminDashController>> _mockLogger;
    private AdminDashController _adminDashController;

    public AdminDashControllerTests()
    {
        _mockPostRepository = new Mock<IPostRepository>();
        _mockLogger = new Mock<ILogger<AdminDashController>>();
        _adminDashController = new AdminDashController(_mockPostRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task TestIndex()
    {
        // Arrange
        var postList = new List<Post>
        {
            new Post { Id = 1, Title = "Post 1", Content = "Content 1", Author = "John Doe", Tag = "Health" },
            new Post { Id = 2, Title = "Post 2", Content = "Content 2", Author = "Jane Smith", Tag = "Test" }
        };
        _mockPostRepository.Setup(repo => repo.GetAllPostsAsync()).ReturnsAsync(postList);

        // Act
        var result = await _adminDashController.Index();

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
            ImageUrl = "https://example.com/image.jpg"
        };
        _mockPostRepository.Setup(repo => repo.AddPostAsync(It.IsAny<Post>())).ReturnsAsync(false);
        SetUser("Test User");

        // Act
        var result = await _adminDashController.Create(testPostViewModel);

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
        var result = await _adminDashController.Details(1);

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
        var result = await _adminDashController.Details(1);

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
        var result = await _adminDashController.Edit(1);

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
        var result = await _adminDashController.Edit(1);

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
        var result = await _adminDashController.Edit(1);

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
        _adminDashController.ControllerContext.HttpContext = new DefaultHttpContext { User = principal };
    }
}
