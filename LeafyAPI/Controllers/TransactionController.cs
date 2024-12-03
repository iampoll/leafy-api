using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LeafyAPI.Models;
using LeafyAPI.Data;
using Microsoft.AspNetCore.Identity;
using LeafyAPI.DTOs;

namespace LeafyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            return Ok(transaction);
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
    }
}