using Microsoft.AspNetCore.Mvc;
using LeafyAPI.Services.Interfaces;
using LeafyAPI.DTOs;

namespace LeafyAPI.Controllers
{
    [ApiController]
    [Route("api/levels")]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _levelService;

        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpGet("leaderboard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LeaderboardResponseDto>>> GetLeaderboard(
            [FromQuery] int? limit)
        {
            var leaderboard = await _levelService.GetLeaderboardAsync(limit);
            return Ok(leaderboard);
        }
    }
} 