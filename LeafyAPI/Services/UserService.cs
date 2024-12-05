using LeafyAPI.DTOs.User;
using LeafyAPI.Repositories.Interfaces;
using LeafyAPI.Services.Interfaces;

namespace LeafyAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserResponseDto?> GetUserInfoAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new GetUserResponseDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                IsOnboarded = user.isOnboarded
            };
        }
    }
}
