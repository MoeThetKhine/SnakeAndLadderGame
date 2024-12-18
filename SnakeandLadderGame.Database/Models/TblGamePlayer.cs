namespace SnakeandLadderGame.Database.Models;

public partial class TblGamePlayer
{
    public string GamePlayerId { get; set; } = null!;

    public string GameId { get; set; } = null!;

    public string PlayerId { get; set; } = null!;

    public string? Color { get; set; }

    public int CurrentPosition { get; set; }

    public string? PlayerStatus { get; set; }

    public virtual TblGame Game { get; set; } = null!;

    public virtual TblPlayer Player { get; set; } = null!;
}
