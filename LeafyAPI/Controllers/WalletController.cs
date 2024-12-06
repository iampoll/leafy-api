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
        private readonly ILevelService _levelService;

        public WalletController(IWalletService walletService, UserManager<User> userManager, ILevelService levelService)
        {
            _walletService = walletService;
            _userManager = userManager;
            _levelService = levelService;
        }

        [HttpPost]
        public async Task<ActionResult<WalletResponseDto>> CreateWallet([FromBody] CreateWalletRequestDto request)
        {
            var user = await _userManager.GetUserAsync(User)
                ?? throw new UnauthorizedAccessException("Please login to access this resource");

            var result = await _walletService.CreateWalletAsync(user.Id, request);
            var initializeLevel = await _levelService.InitializeLevelAsync(new Level { UserId = user.Id });
            
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<WalletResponseDto>> GetWallet()
        {
            var user = await _userManager.GetUserAsync(User) 
                ?? throw new UnauthorizedAccessException("Please login to access this resource");

            var result = await _walletService.GetWalletAsync(user.Id);
            return Ok(result);
        }
    }
}
