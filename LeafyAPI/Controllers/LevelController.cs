using LeafyAPI.Models;
using LeafyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeafyAPI.Controllers
{
    [ApiController]
    [Route("api/levels")]
    [Authorize]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _levelService;

        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }

       
    }
}
