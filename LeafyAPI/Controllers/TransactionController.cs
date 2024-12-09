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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionDto>> CreateTransaction([FromBody] CreateTransactionRequestDto request)
        {
            var user = await _userManager.GetUserAsync(User)
                ?? throw new UnauthorizedAccessException();

            var result = await _transactionService.CreateTransactionAsync(user.Id, request);

            return CreatedAtAction(nameof(GetTransactions), new { id = result.Transaction.Id }, result);
        }

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TransactionCategoryResponseDto>> GetCategories()
        {
            var categories = _transactionService.GetCategories();
            return Ok(categories);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions()
        {
            var user = await _userManager.GetUserAsync(User)
                ?? throw new UnauthorizedAccessException();

            var transactions = await _transactionService.GetTransactionsAsync(user.Id);
            return Ok(transactions);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var user = await _userManager.GetUserAsync(User)
                ?? throw new UnauthorizedAccessException();

            await _transactionService.DeleteTransactionAsync(user.Id, id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionDto>> UpdateTransaction(int id, [FromBody] UpdateTransactionRequestDto request)
        {
            var user = await _userManager.GetUserAsync(User)
                ?? throw new UnauthorizedAccessException();

            var result = await _transactionService.UpdateTransactionAsync(user.Id, id, request);
            return Ok(result);
        }
    }
}