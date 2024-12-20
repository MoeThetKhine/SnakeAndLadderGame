﻿namespace SnakeandLadderGame.Database.Models;

#region TblCell

public partial class TblCell
{
    public string CellId { get; set; } = null!;

    public string BoardId { get; set; } = null!;

    public int CellNo { get; set; }

    public string? CellType { get; set; }

    public int? DestinationCell { get; set; }

    public virtual TblBoard Board { get; set; } = null!;
}

#endregion
