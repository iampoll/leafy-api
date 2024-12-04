using LeafyAPI.Models;
using System.Text.Json.Serialization;

namespace LeafyAPI.DTOs
{
    public class CreateTransactionDto
    {
        public required bool isExpense { get; set; }
        public required decimal Amount { get; set; }
        
        [JsonIgnore] // This will be set through CategoryString if needed
        public TransactionCategory Category { get; set; }
        
        [JsonPropertyName("category")]
        public string? CategoryString { get; set; }
    }
} 