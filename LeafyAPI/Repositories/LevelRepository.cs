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
    }
}