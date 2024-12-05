using LeafyAPI.DTOs.User;

namespace LeafyAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<GetUserResponseDto?> GetUserInfoAsync(string userId);
    }
}
