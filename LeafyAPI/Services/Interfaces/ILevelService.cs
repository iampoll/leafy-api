using LeafyAPI.DTOs.User;
using LeafyAPI.Models;

namespace LeafyAPI.Services.Interfaces
{
    public interface ILevelService
    {
        Task<LevelResponseDto> InitializeLevelAsync(Level level);
        Task<LevelResponseDto> AddExperienceAsync(string userId);
    }
}
