using LeafyAPI.Models;

namespace LeafyAPI.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetByWalletIdAsync(int walletId);
        Task<Transaction?> GetByIdAndWalletIdAsync(int id, int walletId);
        Task DeleteAsync(Transaction transaction);
        Task SaveChangesAsync();
    }
}
