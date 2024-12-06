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
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<User> userManager, ILogger<UserService> logger, IUserRepository userRepository)
        {
            _userManager = userManager;
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<GetUserResponseDto?> GetUserInfoAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new UnauthorizedAccessException();

            _logger.LogInformation("User: {user}", user);

            return new GetUserResponseDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                IsOnboarded = user.isOnboarded,
                Name = user.Name,
                NameSlug = user.NameSlug
            };
        }

        public async Task<GetUserByNameSlugResponseDto?> GetUserByNameSlugAsync(string nameSlug)
        {
            var user = await _userRepository.GetUserByNameSlugAsync(nameSlug)
                ?? throw new UnauthorizedAccessException();

            return new GetUserByNameSlugResponseDto
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
