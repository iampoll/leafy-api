using LeafyAPI.DTOs.User;
using LeafyAPI.Models;

namespace LeafyAPI.Services.Interfaces
{
    public interface ILevelService
    {
        Task<Level> InitializeLevelAsync(Level level);
    }
}
