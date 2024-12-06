using LeafyAPI.DTOs.User;
using LeafyAPI.Helpers;
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
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException();

            var userInfo = await _userService.GetUserInfoAsync(userId);

            return Ok(userInfo);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoRequestDto request)
        {
            var user = await _userManager.GetUserAsync(User)
                ?? throw new UnauthorizedAccessException();

            if (request.Name == null)
                throw new InvalidOperationException("Name is required");

            var nameSlug = GenerateSlug.CreateSlug(request.Name);

            var existingUser = await _userManager.Users.Where(u => u.NameSlug == nameSlug).FirstOrDefaultAsync();
            if (existingUser != null)
                throw new InvalidOperationException("Name already taken");

            user.Name = request.Name;
            user.NameSlug = nameSlug;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpGet("{nameSlug}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserByNameSlug(string nameSlug)
        {
            var user = await _userService.GetUserByNameSlugAsync(nameSlug)
                ?? throw new KeyNotFoundException("User not found");

            return Ok(user);
        }
    }
}
