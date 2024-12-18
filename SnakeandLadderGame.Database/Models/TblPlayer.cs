namespace SnakeandLadderGame.Database.Models;

public partial class TblPlayer
{
    public string PlayerId { get; set; } = null!;

    public string PlayerName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<TblGamePlayer> TblGamePlayers { get; set; } = new List<TblGamePlayer>();

    public virtual ICollection<TblGameWinner> TblGameWinners { get; set; } = new List<TblGameWinner>();
}
