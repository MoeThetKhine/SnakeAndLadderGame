namespace SnakeandLadderGame.Database.Models;

public partial class TblBoard
{
    public string BoardId { get; set; } = null!;

    public int Size { get; set; }

    public virtual ICollection<TblCell> TblCells { get; set; } = new List<TblCell>();

    public virtual ICollection<TblGame> TblGames { get; set; } = new List<TblGame>();
}
