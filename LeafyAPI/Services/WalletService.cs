using LeafyAPI.DTOs;
using LeafyAPI.Models;
using LeafyAPI.Repositories.Interfaces;
using LeafyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LeafyAPI.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly UserManager<User> _userManager;

        public WalletService(IWalletRepository walletRepository, UserManager<User> userManager)
        {
            _walletRepository = walletRepository;
            _userManager = userManager;
        }

        public async Task<WalletResponseDto> CreateWalletAsync(string userId, CreateWalletRequestDto createWalletDto)
        {
            var existingWallet = await _walletRepository.GetByUserIdAsync(userId);
            if (existingWallet != null)
            {
                throw new InvalidOperationException("User already has a wallet");
            }

            var wallet = new Wallet
            {
                UserId = userId,
                Balance = createWalletDto.InitialBalance
            };

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.isOnboarded = true;
                await _userManager.UpdateAsync(user);
            }

            await _walletRepository.CreateAsync(wallet);
            await _walletRepository.SaveChangesAsync();

            return new WalletResponseDto
            {
                Balance = wallet.Balance,
            };
        }

        public async Task<WalletResponseDto> GetWalletAsync(string userId)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(userId)
                ?? throw new KeyNotFoundException("Wallet not found");

            return new WalletResponseDto
            {
                Balance = wallet.Balance,
            };
        }
    }
}
