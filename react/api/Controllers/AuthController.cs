using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: api/auth/user
        [HttpGet("user")]
        [Authorize]
        public IActionResult GetUserIdentity()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new { name = User.Identity.Name });
            }
            return Unauthorized(new { message = "User is not authenticated." });
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser { UserName = registerRequest.Email, Email = registerRequest.Email };
            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User registered successfully.");
                return Ok(new { Message = "Registration successful" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Registration", error.Description);
            }

            return BadRequest(ModelState);
        }

        // GET: api/auth/user/role
        [HttpGet("user/role")]
        [Authorize]
        public IActionResult GetUserRole()
        {
            if (User.Identity.IsAuthenticated)
            {
                var role = User.IsInRole("Admin") ? "Admin" : "User"; // Adjust roles as needed

                return Ok(new { role });
            }

            return Unauthorized(new { message = "User is not authenticated." });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, loginRequest.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return Ok(new { Message = "Login successful" });
            }
            if (result.RequiresTwoFactor)
            {
                return Unauthorized(new { Message = "Requires two-factor authentication" });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return Unauthorized(new { Message = "Account locked out" });
            }

            return Unauthorized(new { Message = "Invalid login attempt" });
        }

        // POST: api/auth/logout
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Ok(new { Message = "Logout successful" });
        }

        // GET: api/auth/user/details
        [HttpGet("user/details")]
        [Authorize]
        public async Task<IActionResult> GetUserDetails()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    return Ok(new
                    {
                        username = user.UserName,
                        email = user.Email,
                        role = User.IsInRole("Admin") ? "Admin" : "User" // Adjust roles as needed
                    });
                }
            }
            return Unauthorized(new { message = "User is not authenticated." });
        }
    }


    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }

}
