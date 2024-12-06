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

        public async Task<LevelResponseDto> InitializeLevelAsync(Level level)
        {
            var initializedLevel = await _levelRepository.InitializeLevelAsync(level);
            await _levelRepository.SaveChangesAsync();

            return MapToDto(initializedLevel);
        }

        private static LevelResponseDto MapToDto(Level level)
        {
            return new LevelResponseDto
            {
                CurrentLevel = level.CurrentLevel,
                ExperiencePoints = level.ExperiencePoints,
                ExperienceThreshold = level.ExperienceThreshold
            };
        }
    }
}
