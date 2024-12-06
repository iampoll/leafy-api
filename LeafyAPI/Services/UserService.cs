using LeafyAPI.DTOs.User;
using LeafyAPI.Models;
using LeafyAPI.Repositories.Interfaces;
using LeafyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LeafyAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUserResponseDto?> GetUserInfoAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new UnauthorizedAccessException();

            return new GetUserResponseDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                IsOnboarded = user.isOnboarded,
                Name = user.Name ?? string.Empty
            };
        }

        public async Task<GetUserByNameResponseDto?> GetUserByNameAsync(string name)
        {
            var user = await _userManager.FindByNameAsync(name)
                ?? throw new UnauthorizedAccessException();

            return new GetUserByNameResponseDto
            {
                Name = user.Name,
                Level = new LevelResponseDto
                {
                    CurrentLevel = user.Level.CurrentLevel,
                    ExperiencePoints = user.Level.ExperiencePoints,
                    ExperienceThreshold = user.Level.ExperienceThreshold
                }
            };
        }
    }
}
