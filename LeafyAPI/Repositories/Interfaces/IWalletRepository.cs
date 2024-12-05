using LeafyAPI.Models;

namespace LeafyAPI.Repositories.Interfaces
{
    public interface IWalletRepository
    {
        Task<Wallet?> GetByUserIdAsync(string userId);
        Task<Wallet> CreateAsync(Wallet wallet);
        Task<Wallet> UpdateAsync(Wallet wallet);
        Task SaveChangesAsync();
    }
}
