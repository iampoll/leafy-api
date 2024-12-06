using LeafyAPI.Models;

namespace LeafyAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByNameSlugAsync(string nameSlug);
    }
}
