namespace SnakeAndLadderGame.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly PlayerService _playerService;

    public PlayerController(PlayerService playerService)
    {
        _playerService = playerService;
    }

    #region CreatePlayer

    [HttpPost("create")]
    public async Task<IActionResult> CreatePlayerAsync([FromBody] PlayerRequestModel request)
    {
        var result = await _playerService.CreatePlayerAsync(request);

        return Ok(result);
    }

    #endregion

    #region GetPlayerAsync

    [HttpGet]
    public async Task<IActionResult> GetPlayerAsync()
    {
        var lst = await _playerService.GetPlayerListAsync();
        return Ok(lst);
    }

    #endregion

}
