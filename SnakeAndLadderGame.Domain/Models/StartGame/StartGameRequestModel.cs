using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadderGame.Domain.Models.StartGame
{
    public class StartGameRequestModel
    {
        public string BoardID { get; set; }
        public List<string> PlayerIDs { get; set; }
    }
}
