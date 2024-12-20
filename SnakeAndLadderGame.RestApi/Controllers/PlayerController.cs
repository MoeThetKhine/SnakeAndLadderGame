using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakeAndLadderGame.Domain.Features.Player;
using SnakeAndLadderGame.Domain.Models.Player;

namespace SnakeAndLadderGame.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public PlayerController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePlayer([FromBody] PlayerRequestModel request)
        {
           

            var result = await _playerService.CreatePlayerAsync(request);

            return Ok(result);
        }
    }
}
