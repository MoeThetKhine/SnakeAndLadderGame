using SnakeAndLadderGame.Domain.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame.Domain.Models.StartGame
{
    public class StartGameResponseModel
    {
        public string GameID { get; set; }
        public string BoardID { get; set; }
        public List<PlayerDetailModel> Players { get; set; }
    }
}
