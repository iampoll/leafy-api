using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LeafyAPI.Services.Interfaces;
using LeafyAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using LeafyAPI.Models;

namespace LeafyAPI.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly UserManager<User> _userManager;

        public TransactionController(ITransactionService transactionService, UserManager<User> userManager)
        {
            _transactionService = transactionService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> CreateTransaction([FromBody] CreateTransactionRequestDto request)
        {
            var user = await _userManager.GetUserAsync(User)
                ?? throw new UnauthorizedAccessException();

            var result = await _transactionService.CreateTransactionAsync(user.Id, request);
            return Ok(result);
        }

        [HttpGet("categories")]
        public ActionResult<IEnumerable<TransactionCategoryResponseDto>> GetCategories()
        {
            var categories = _transactionService.GetCategories();
            return Ok(categories);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions()
        {
            var user = await _userManager.GetUserAsync(User)
                ?? throw new UnauthorizedAccessException();

            var transactions = await _transactionService.GetTransactionsAsync(user.Id);
            return Ok(transactions);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var user = await _userManager.GetUserAsync(User)
                ?? throw new UnauthorizedAccessException();

            await _transactionService.DeleteTransactionAsync(user.Id, id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<TransactionDto>> UpdateTransaction(int id, [FromBody] UpdateTransactionRequestDto request)
        {
            var user = await _userManager.GetUserAsync(User)
                ?? throw new UnauthorizedAccessException();

            var result = await _transactionService.UpdateTransactionAsync(user.Id, id, request);
            return Ok(result);
        }
    }
}