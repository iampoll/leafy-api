using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LeafyAPI.Models;
using LeafyAPI.Data;
using Microsoft.AspNetCore.Identity;
using LeafyAPI.DTOs;

namespace LeafyAPI.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public TransactionController(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var wallet = _context.Wallets.FirstOrDefault(w => w.UserId == user.Id);
            if (wallet == null)
                return NotFound("Wallet not found");

            if ((int)request.Category < 1 || (int)request.Category > 15)
                return BadRequest("Invalid category");

            var transaction = new Transaction
            {
                WalletId = wallet.Id,
                isExpense = request.isExpense,
                Amount = request.Amount,    
                Category = request.Category
            };

            if (request.isExpense)
                wallet.Balance -= request.Amount;
            else
                wallet.Balance += request.Amount;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Category = transaction.Category,
                CategoryName = transaction.Category.ToString(),
                isExpense = transaction.isExpense,
                CreatedAt = transaction.CreatedAt
            });
        }

        [HttpGet("categories")]
        public ActionResult<IEnumerable<TransactionCategoryDto>> GetCategories()
        {
            var categories = Enum.GetValues<TransactionCategory>()
                .Select(c => new TransactionCategoryDto
                {
                    Id = (int)c,
                    Name = c.ToString()
                });

            return Ok(categories);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var wallet = _context.Wallets.FirstOrDefault(w => w.UserId == user.Id); 
            if (wallet == null)
                return NotFound("Wallet not found");

            var transactions = _context.Transactions
                .Where(t => t.WalletId == wallet.Id)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Category = t.Category,
                    CategoryName = t.Category.ToString(),
                    isExpense = t.isExpense,
                    CreatedAt = t.CreatedAt
                })
                .ToList();

            return Ok(transactions);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var wallet = _context.Wallets.FirstOrDefault(w => w.UserId == user.Id);
            if (wallet == null)
                return NotFound("Wallet not found");

            var transaction = _context.Transactions.FirstOrDefault(t => t.Id == id && t.WalletId == wallet.Id); 
            if (transaction == null)
                return NotFound("Transaction not found");

            if (transaction.isExpense)
                wallet.Balance += transaction.Amount;   
            else
                wallet.Balance -= transaction.Amount;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] UpdateTransactionDto request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var wallet = _context.Wallets.FirstOrDefault(w => w.UserId == user.Id);
            if (wallet == null)
                return NotFound("Wallet not found");

            var transaction = _context.Transactions.FirstOrDefault(t => t.Id == id && t.WalletId == wallet.Id);
            if (transaction == null)
                return NotFound("Transaction not found");

            // First, reverse the effect of the old transaction
            if (transaction.isExpense)
                wallet.Balance += transaction.Amount;  // Add back the old expense
            else
                wallet.Balance -= transaction.Amount;  // Subtract the old income

            // Then apply the new transaction
            if (request.isExpense)
                wallet.Balance -= request.Amount;  // Subtract the new expense
            else
                wallet.Balance += request.Amount;  // Add the new income

            // Update transaction details
            transaction.Amount = request.Amount;
            transaction.Category = request.Category;
            transaction.isExpense = request.isExpense;

            await _context.SaveChangesAsync();
            
            return Ok(new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Category = transaction.Category,
                CategoryName = transaction.Category.ToString(),
                isExpense = transaction.isExpense,
                CreatedAt = transaction.CreatedAt
            });
        }

    }
}