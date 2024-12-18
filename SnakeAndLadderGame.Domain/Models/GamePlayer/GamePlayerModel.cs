namespace SnakeAndLadderGame.Domain.Models.GamePlayer
{
    public class GamePlayerModel
    {
        public string GamePlayerId { get; set; } = null!;

        public string GameId { get; set; } = null!;

        public string PlayerId { get; set; } = null!;

        public string? Color { get; set; }

        public int CurrentPosition { get; set; }

        public string? PlayerStatus { get; set; }
    }
}
