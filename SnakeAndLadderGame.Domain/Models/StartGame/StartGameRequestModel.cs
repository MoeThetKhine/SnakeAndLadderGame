namespace SnakeAndLadderGame.Domain.Models.StartGame;

#region StartGameRequestModel

public class StartGameRequestModel
{
    public string BoardID { get; set; }
    public List<string> PlayerIDs { get; set; }
}

#endregion
