using LeafyAPI.DTOs.User;
using LeafyAPI.Models;
using LeafyAPI.Repositories.Interfaces;
using LeafyAPI.Services.Interfaces;

namespace LeafyAPI.Services
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _levelRepository;

        public LevelService(ILevelRepository levelRepository)
        {
            _levelRepository = levelRepository;
        }

        public async Task<Level> InitializeLevelAsync(Level level)
        {
            await _levelRepository.InitializeLevelAsync(level);
            await _levelRepository.SaveChangesAsync();
            return level;
        }
    }
}
