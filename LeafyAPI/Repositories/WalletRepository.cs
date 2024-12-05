using LeafyAPI.Data;
using LeafyAPI.Models;
using LeafyAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeafyAPI.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly DataContext _context;

        public WalletRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Wallet?> GetByUserIdAsync(string userId)
        {
            return await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task<Wallet> CreateAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            return wallet;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
