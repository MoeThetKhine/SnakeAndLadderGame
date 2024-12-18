namespace SnakeAndLadderGame.Domain.Models.StartGame
{
    public class StartGameRequestModel
    {
        public string BoardID { get; set; }
        public List<string> PlayerIDs { get; set; }
    }
}
