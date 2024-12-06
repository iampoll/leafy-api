using LeafyAPI.Models;

namespace LeafyAPI.Repositories.Interfaces
{
    public interface ILevelRepository
    {
        Task<Level> InitializeLevelAsync(Level level);
        Task SaveChangesAsync();
    }
}
