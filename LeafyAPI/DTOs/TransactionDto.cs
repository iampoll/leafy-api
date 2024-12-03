using LeafyAPI.Models;

namespace LeafyAPI.DTOs
{
    public class TransactionDto
    {
        public required int Id { get; set; }
        public required decimal Amount { get; set; }
        public required TransactionCategory Category { get; set; }
        public required string CategoryName { get; set; }
        public required bool isExpense { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}   