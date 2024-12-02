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
    public class WalletController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public WalletController(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDto request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var existingWallet = _context.Wallets.FirstOrDefault(w => w.UserId == user.Id);
            if (existingWallet != null)
                return BadRequest("User already has a wallet");

            var wallet = new Wallet
            {
                UserId = user.Id,
                Balance = request.InitialBalance
            };

            user.isOnboarded = true;    
            await _userManager.UpdateAsync(user);
            _context.Users.Update(user);


            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();

            return Ok(wallet);
        }

        [HttpGet]
        public async Task<IActionResult> GetWallet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var wallet = _context.Wallets.FirstOrDefault(w => w.UserId == user.Id);
            if (wallet == null)
                return NotFound();

            return Ok(wallet);
        }
    }
}
