using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LeafyAPI.Services.Interfaces;
using LeafyAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using LeafyAPI.Models;

namespace LeafyAPI.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly UserManager<User> _userManager;

        public WalletController(IWalletService walletService, UserManager<User> userManager)
        {
            _walletService = walletService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<WalletResponseDto>> CreateWallet([FromBody] CreateWalletRequestDto request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            try
            {
                var result = await _walletService.CreateWalletAsync(user.Id, request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<WalletResponseDto>> GetWallet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            try
            {
                var result = await _walletService.GetWalletAsync(user.Id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Wallet not found");
            }
        }
    }
}
