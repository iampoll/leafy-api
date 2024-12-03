using LeafyAPI.Models;

namespace LeafyAPI.DTOs
{
    public class CreateTransactionDto
    {
        public required bool isExpense {get; set;}
        public required decimal Amount { get; set; }
    }
} 