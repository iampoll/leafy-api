using LeafyAPI.Models;

namespace LeafyAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(string userId);
        Task<User?> GetUserByNameAsync(string name);
    }
}
