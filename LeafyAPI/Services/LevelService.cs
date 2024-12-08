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

        public async Task<LevelResponseDto> AddExperienceAsync(string userId)
        {
            var level = await _levelRepository.GetLevelByUserIdAsync(userId)
                ?? throw new KeyNotFoundException("Level not found");

            const int experienceToAdd = 10;
            level.ExperiencePoints += experienceToAdd;

            var levelUp = level.ExperiencePoints >= level.ExperienceThreshold;
            if (levelUp)
            {
                level.CurrentLevel++;
                level.ExperiencePoints = 0;
                level.ExperienceThreshold = CalculateNextThreshold(level.CurrentLevel);
            }

            await _levelRepository.SaveChangesAsync();
            return MapToDto(level);
        }

        private static int CalculateNextThreshold(int currentLevel)
        {
            var increaseByTenPercent = 1.1;
            var nextLevelThreshold = (int)(100 * Math.Pow(increaseByTenPercent, currentLevel - 1));
            return nextLevelThreshold;
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
