namespace LeafyAPI.DTOs
{
    public class CreateTransactionResponseDto
    {
        public required IsLeveledUpResponseDto IsLeveledUp { get; set; }
        public required TransactionDto Transaction { get; set; }
    }
} 