using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using WebMVC.Controllers;
using WebMVC.Models;
using WebMVC.DAL;
using WebMVC.ViewModels;
using Xunit;

namespace WebMVC.Test.Controllers
{
    public class CommentControllerTests
    {
        private Mock<ICommentRepository> _mockCommentRepository;
        private Mock<ILogger<CommentController>> _mockLogger;
        private CommentController _commentController;

        public CommentControllerTests()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();
            _mockLogger = new Mock<ILogger<CommentController>>();
            _commentController = new CommentController(_mockCommentRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task TestCreate_ValidComment()
        {
            // Arrange
            var commentModel = new CommentCreateViewModel
            {
                Text = "This is a test comment.",
                PostId = 1
            };
            SetUser("TestUser");

            _mockCommentRepository
                .Setup(repo => repo.AddCommentAsync(It.IsAny<Comment>()))
                .ReturnsAsync(true);

            // Act
            var result = await _commentController.Create(commentModel);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirectResult.ActionName);
            Assert.Equal("Post", redirectResult.ControllerName);
        }

        [Fact]
        public async Task TestCreate_InvalidModelState()
        {
            // Arrange
            var commentModel = new CommentCreateViewModel();
            _commentController.ModelState.AddModelError("Text", "Text is required");
            SetUser("TestUser");

            // Act
            var result = await _commentController.Create(commentModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TestDelete_ValidComment_Authorized()
        {
            // Arrange
            var comment = new Comment
            {
                Id = 1,
                Author = "TestUser",
                PostId = 1
            };
            SetUser("TestUser");

            _mockCommentRepository.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync(comment);
            _mockCommentRepository.Setup(repo => repo.DeleteCommentAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _commentController.Delete(1);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirectResult.ActionName);
            Assert.Equal("Post", redirectResult.ControllerName);
        }

        [Fact]
        public async Task TestDelete_CommentNotFound()
        {
            // Arrange
            _mockCommentRepository.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync((Comment?)null);

            // Act
            var result = await _commentController.Delete(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task TestEdit_ValidComment_Authorized()
        {
            // Arrange
            var comment = new Comment
            {
                Id = 1,
                Author = "TestUser",
                Text = "Original text",
                PostId = 1
            };
            var model = new CommentEditViewModel
            {
                Text = "Updated text"
            };
            SetUser("TestUser");

            _mockCommentRepository.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync(comment);
            _mockCommentRepository.Setup(repo => repo.UpdateCommentAsync(It.IsAny<Comment>())).ReturnsAsync(true);

            // Act
            var result = await _commentController.Edit(1, model);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirectResult.ActionName);
            Assert.Equal("Post", redirectResult.ControllerName);
        }

        [Fact]
        public async Task TestEdit_CommentNotAuthorized()
        {
            // Arrange
            var comment = new Comment
            {
                Id = 1,
                Author = "OtherUser",
                PostId = 1
            };
            var model = new CommentEditViewModel
            {
                Text = "Updated text"
            };
            SetUser("TestUser");

            _mockCommentRepository.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync(comment);

            // Act
            var result = await _commentController.Edit(1, model);

            // Assert
            Assert.IsType<ForbidResult>(result);
        }

        private void SetUser(string userName, string? role = null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName)
            };

            if (!string.IsNullOrEmpty(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);
            _commentController.ControllerContext.HttpContext = new DefaultHttpContext { User = principal };
        }
    }
}
