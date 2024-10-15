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
    [Fact]
    public async Task TestIndex()
    {
        // arrange
        var postList = new List<Post>()
        {
            new Post 
            { 
                Id = 1, 
                Title = "Post 1", 
                Content = "Content 1",
                Author = "John Doe",
                ImageUrl = "https://miro.medium.com/v2/resize:fit:1200/1*uNCVd_VqFOcdxhsL71cT5Q.jpeg"
            },
            new Post
            {
                Id = 2, 
                Title = "Post 2", 
                Content = "Content 2",
                Author = "Jane Smith",
                ImageUrl = "https://t3.ftcdn.net/jpg/07/46/74/96/360_F_746749607_zDV4D3BHULyb1NVvRVWujOSwNJWv8ufK.jpg"
            }
        };

        var mockPostRepository = new Mock<IPostRepository>();
        mockPostRepository.Setup(repo => repo.GetAllPostsAsync()).ReturnsAsync(postList);
        var mockLogger = new Mock<ILogger<PostController>>();
        var postController = new PostController(mockPostRepository.Object, mockLogger.Object);

        // act
        var result = await postController.Index();

        // assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Post>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count());
        Assert.Equal(postList, model);
    }

    [Fact]
    public async Task TestCreateNotOk()
    {
        // arrange
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


        // act
        var result = await postController.Create(testPostViewModel); // Pass the view model directly

        // assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<PostCreateViewModel>(viewResult.ViewData.Model);
        Assert.Equal(testPostViewModel.Title, model.Title);
        Assert.Equal(testPostViewModel.Content, model.Content);
        Assert.Equal(testPostViewModel.ImageUrl, model.ImageUrl);
    }
}
