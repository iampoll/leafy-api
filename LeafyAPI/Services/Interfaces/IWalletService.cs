using LeafyAPI.DTOs;
using LeafyAPI.Models;

namespace LeafyAPI.Services.Interfaces
{
    public interface IWalletService
    {
        Task<WalletResponseDto> CreateWalletAsync(string userId, CreateWalletRequestDto createWalletDto);
        Task<WalletResponseDto> GetWalletAsync(string userId);
    }
}
