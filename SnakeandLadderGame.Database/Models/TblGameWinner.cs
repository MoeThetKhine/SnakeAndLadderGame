﻿namespace SnakeandLadderGame.Database.Models;

#region TblGameWinner

public partial class TblGameWinner
{
    public string GameWinnerId { get; set; } = null!;

    public string GameId { get; set; } = null!;

    public string PlayerId { get; set; } = null!;

    public int Rank { get; set; }

    public virtual TblGame Game { get; set; } = null!;

    public virtual TblPlayer Player { get; set; } = null!;
}

#endregion
