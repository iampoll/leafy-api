using LeafyAPI.DTOs.User;
using LeafyAPI.Models;
using LeafyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeafyAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            // var userInfo = await _userService.GetUserInfoAsync(userId);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var userInfo = new GetUserResponseDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                IsOnboarded = user.isOnboarded,
                Name = user.Name ?? string.Empty
            };

            return Ok(userInfo);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoRequestDto request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            if (request.Name == null)
                return BadRequest("Name is required");

            var existingUser = await _userManager.Users.Where(u => u.Name == request.Name).FirstOrDefaultAsync();
            if (existingUser != null)
                return BadRequest("Name already taken");

            user.Name = request.Name;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var user = await _userService.GetUserByNameAsync(name);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }
    }
}
