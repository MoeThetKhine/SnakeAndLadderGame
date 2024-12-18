namespace SnakeandLadderGame.Database.Models;

public partial class TblGame
{
    public string GameId { get; set; } = null!;

    public string BoardId { get; set; } = null!;

    public string? GameStatus { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual TblBoard Board { get; set; } = null!;

    public virtual ICollection<TblGamePlayer> TblGamePlayers { get; set; } = new List<TblGamePlayer>();

    public virtual ICollection<TblGameWinner> TblGameWinners { get; set; } = new List<TblGameWinner>();
}
