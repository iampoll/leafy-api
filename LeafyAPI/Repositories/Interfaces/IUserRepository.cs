using LeafyAPI.Models;

namespace LeafyAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByNameAsync(string name);
    }
}
