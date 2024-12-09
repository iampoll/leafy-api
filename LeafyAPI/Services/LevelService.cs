using LeafyAPI.Models;
using LeafyAPI.Repositories.Interfaces;
using LeafyAPI.Services.Interfaces;
using LeafyAPI.DTOs;

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

        public async Task<IEnumerable<LeaderboardResponseDto>> GetLeaderboardAsync(int? limit = 10)
        {
            var levels = await _levelRepository.GetLeaderboardAsync(limit);
            var rank = 1;

            return levels.Select(l => new LeaderboardResponseDto
            {
                Name = l.User.Name,
                NameSlug = l.User.NameSlug,
                CurrentLevel = l.CurrentLevel,
                ExperiencePoints = l.ExperiencePoints,
                TotalExperiencePoints = CalculateTotalExperience(l.CurrentLevel, l.ExperiencePoints),
                Rank = rank++
            });
        }

        public async Task<IsLeveledUpResponseDto?> IsLeveledUpAsync(string userId)
        {
            var level = await _levelRepository.GetLevelByUserIdAsync(userId)
                ?? throw new KeyNotFoundException("Level not found");

            var isLeveledUp = level.ExperiencePoints == 0;
            
            return new IsLeveledUpResponseDto { IsLeveledUp = isLeveledUp, Level = MapToDto(level) };
        }

        private static int CalculateNextThreshold(int currentLevel)
        {
            var increaseByTenPercent = 1.1;
            var nextLevelThreshold = (int)(100 * Math.Pow(increaseByTenPercent, currentLevel - 1));
            return nextLevelThreshold;
        }

        private int CalculateTotalExperience(int currentLevel, int currentXP)
        {
            var total = currentXP;
            for (var level = 1; level < currentLevel; level++)
            {
                total += CalculateNextThreshold(level);
            }
            return total;
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
