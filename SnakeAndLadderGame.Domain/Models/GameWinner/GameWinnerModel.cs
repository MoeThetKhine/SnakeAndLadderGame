namespace SnakeAndLadderGame.Domain.Models.GameWinner;

#region GameWinnerModel

public class GameWinnerModel
{
    public string GameWinnerId { get; set; } = null!;

    public string GameId { get; set; } = null!;

    public string PlayerId { get; set; } = null!;

    public int Rank { get; set; }
}

#endregion
