using SnakeandLadderGame.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame.Domain.Models.GameWinner
{
    public class GameWinnerModel
    {
        public string GameWinnerId { get; set; } = null!;

        public string GameId { get; set; } = null!;

        public string PlayerId { get; set; } = null!;

        public int Rank { get; set; }

    }
}
