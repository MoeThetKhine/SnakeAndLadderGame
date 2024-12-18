namespace SnakeAndLadderGame.Domain.Models.Game;

public class GameModel
{
    public string GameId { get; set; } = null!;

    public string BoardId { get; set; } = null!;

    public string? GameStatus { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
