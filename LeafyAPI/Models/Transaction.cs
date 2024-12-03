using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeafyAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public required int WalletId { get; set; }

        [ForeignKey("WalletId")]
        public Wallet? Wallet { get; set; }

        public required bool isExpense { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}