using LeafyAPI.Data;
using LeafyAPI.Models;
using LeafyAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeafyAPI.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly DataContext _context;

        public LevelRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Level> InitializeLevelAsync(Level level)
        {
            await _context.Levels.AddAsync(level);
            return level;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Level?> GetLevelByUserIdAsync(string userId)
        {
            return await _context.Levels
                .FirstOrDefaultAsync(l => l.UserId == userId);
        }

        public async Task<IEnumerable<Level>> GetLeaderboardAsync(int? limit = 10)
        {
            var query = _context.Levels
                .Include(l => l.User)
                .OrderByDescending(l => l.CurrentLevel)
                .ThenByDescending(l => l.ExperiencePoints);

            return await (limit.HasValue ? query.Take(limit.Value) : query).ToListAsync();
        }
    }
}