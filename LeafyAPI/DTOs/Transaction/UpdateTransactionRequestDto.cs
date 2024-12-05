using LeafyAPI.Models;

namespace LeafyAPI.DTOs
{
    public class UpdateTransactionRequestDto
    {
        public required bool isExpense { get; set; }
        public required decimal Amount { get; set; }
        public required TransactionCategory Category { get; set; }
    }
} 