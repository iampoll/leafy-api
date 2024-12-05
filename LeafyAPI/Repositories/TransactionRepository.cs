using LeafyAPI.Data;
using LeafyAPI.Models;
using LeafyAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeafyAPI.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;

        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetByWalletIdAsync(int walletId)
        {
            return await _context.Transactions
                .Where(t => t.WalletId == walletId)
                .ToListAsync();
        }

        public async Task<Transaction?> GetByIdAndWalletIdAsync(int id, int walletId)
        {
            return await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == id && t.WalletId == walletId);
        }

        public async Task DeleteAsync(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
