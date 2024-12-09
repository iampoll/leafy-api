using LeafyAPI.DTOs;

namespace LeafyAPI.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<CreateTransactionResponseDto> CreateTransactionAsync(string userId, CreateTransactionRequestDto request);
        Task<IEnumerable<TransactionDto>> GetTransactionsAsync(string userId);
        IEnumerable<TransactionCategoryResponseDto> GetCategories();
        Task DeleteTransactionAsync(string userId, int transactionId);
        Task<TransactionDto> UpdateTransactionAsync(string userId, int transactionId, UpdateTransactionRequestDto request);
    }
}
