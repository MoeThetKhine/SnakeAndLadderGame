namespace SnakeAndLadderGame.Domain.Models.PlayGame;

#region PlayGameResponseModel

public class PlayGameResponseModel
{
    public string PlayerID { get; set; }
    public string Color { get; set; }
    public int DiceRoll { get; set; }
    public int PreviousPosition { get; set; }
    public int NewPosition { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
}

#endregion
