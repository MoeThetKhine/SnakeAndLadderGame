﻿

namespace SnakeAndLadderGame.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;

    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }

    #region StartGame

    [HttpPost("start-game")]
    public async Task<IActionResult> StartGame([FromBody] StartGameRequestModel request)
    {
        var result = await _gameService.StartGameAsync(request);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else if (result.IsValidationError)
        {
            return BadRequest(result);
        }
        else
        {
            return StatusCode(500, result);
        }
    }

    #endregion

    #region PlayGame

    [HttpPost("play")]
    public async Task<IActionResult> PlayGame([FromBody] PlayGameRequestModel request)
    {
        var result = await _gameService.PlayGameAsync(request);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else if (result.IsValidationError)
        {
            return BadRequest(result);
        }
        else
        {
            return StatusCode(500, result);
        }
    }

    #endregion

}
