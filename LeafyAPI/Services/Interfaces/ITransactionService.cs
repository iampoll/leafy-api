using LeafyAPI.DTOs;

namespace LeafyAPI.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionDto> CreateTransactionAsync(string userId, CreateTransactionRequestDto request);
        Task<IEnumerable<TransactionDto>> GetTransactionsAsync(string userId);
        Task<IEnumerable<TransactionCategoryResponseDto>> GetCategoriesAsync();
        Task DeleteTransactionAsync(string userId, int transactionId);
        Task<TransactionDto> UpdateTransactionAsync(string userId, int transactionId, UpdateTransactionRequestDto request);
    }
}
