namespace SnakeAndLadderGame.Domain.Models.StartGame;

#region StartGameResponseModel

public class StartGameResponseModel
{
    public string GameID { get; set; }
    public string BoardID { get; set; }
    public List<PlayerDetailModel> Players { get; set; }
}

#endregion
