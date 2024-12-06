using LeafyAPI.DTOs;
using LeafyAPI.Models;
using LeafyAPI.Repositories.Interfaces;
using LeafyAPI.Services.Interfaces;

namespace LeafyAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;

        public TransactionService(ITransactionRepository transactionRepository, IWalletRepository walletRepository)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        public async Task<TransactionDto> CreateTransactionAsync(string userId, CreateTransactionRequestDto request)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(userId)
                ?? throw new KeyNotFoundException("Wallet not found");

            if ((int)request.Category < 1 || (int)request.Category > 16)
                throw new InvalidDataException("Invalid category");

            if (request.isExpense && request.Category == TransactionCategory.Income)
                throw new InvalidDataException("Income cannot be an expense");

            var transaction = new Transaction
            {
                WalletId = wallet.Id,
                isExpense = request.isExpense,
                Amount = request.Amount,
                Category = request.isExpense ? request.Category : TransactionCategory.Income
            };

            if (request.isExpense)
                wallet.Balance -= request.Amount;
            else
                wallet.Balance += request.Amount;

            await _transactionRepository.CreateAsync(transaction);
            await _transactionRepository.SaveChangesAsync();

            return MapToDto(transaction);
        }

        public IEnumerable<TransactionCategoryResponseDto> GetCategories()
        {
            return Enum.GetValues<TransactionCategory>()
                .Select(c => new TransactionCategoryResponseDto
                {
                    Id = (int)c,
                    Name = c.ToString()
                });
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsAsync(string userId)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(userId)
                ?? throw new KeyNotFoundException("Wallet not found");

            var transactions = await _transactionRepository.GetByWalletIdAsync(wallet.Id);
            return transactions.Select(MapToDto);
        }

        public async Task DeleteTransactionAsync(string userId, int transactionId)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(userId)
                ?? throw new KeyNotFoundException("Wallet not found");

            var transaction = await _transactionRepository.GetByIdAndWalletIdAsync(transactionId, wallet.Id)
                ?? throw new KeyNotFoundException("Transaction not found");

            if (transaction.isExpense)
                wallet.Balance += transaction.Amount;
            else
                wallet.Balance -= transaction.Amount;

            await _transactionRepository.DeleteAsync(transaction);
            await _transactionRepository.SaveChangesAsync();
        }

        public async Task<TransactionDto> UpdateTransactionAsync(string userId, int transactionId, UpdateTransactionRequestDto request)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(userId)
                ?? throw new KeyNotFoundException("Wallet not found");

            var transaction = await _transactionRepository.GetByIdAndWalletIdAsync(transactionId, wallet.Id)
                ?? throw new KeyNotFoundException("Transaction not found");

            if (request.isExpense && request.Category == TransactionCategory.Income)
                throw new InvalidDataException("Income cannot be an expense");

            // Reverse old transaction
            if (transaction.isExpense)
                wallet.Balance += transaction.Amount;
            else
                wallet.Balance -= transaction.Amount;

            // Apply new transaction
            if (request.isExpense)
                wallet.Balance -= request.Amount;
            else
                wallet.Balance += request.Amount;

            transaction.Amount = request.Amount;
            transaction.Category = request.isExpense ? request.Category : TransactionCategory.Income;
            transaction.isExpense = request.isExpense;

            await _transactionRepository.SaveChangesAsync();
            return MapToDto(transaction);
        }

        private static TransactionDto MapToDto(Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Category = transaction.Category,
                CategoryName = transaction.Category.ToString(),
                isExpense = transaction.isExpense,
                CreatedAt = transaction.CreatedAt
            };
        }
    }
}
