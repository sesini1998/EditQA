using CapitalTest.DTOs;
using CapitalTest.IServices;
using CapitalTest.Models;
using CapitalTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            this._authService = authService ?? throw new ArgumentNullException(nameof(authService));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // POST: api/Auth/signup
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] Users user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _authService.SignUp(user);
                return Ok("Signed up successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Sign up.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // POST: api/Auth/signin
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginUserDto credintials)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(credintials.Email) ||
                    string.IsNullOrWhiteSpace(credintials.Password))
                {
                    _logger.LogError("Username or password cannot be empty.");
                    return BadRequest("Username or password cannot be empty.");
                }
                var user = await _authService.SignIn(credintials);
                if (user is null)
                {
                    _logger.LogError("Invalid email or password.");
                    return Unauthorized("Invalid email or password.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Sign in.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
