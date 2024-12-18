using SnakeAndLadderGame.Domain.Models.Player;

namespace SnakeAndLadderGame.Domain.Models.StartGame;

public class StartGameResponseModel
{
    public string GameID { get; set; }
    public string BoardID { get; set; }
    public List<PlayerDetailModel> Players { get; set; }
}
