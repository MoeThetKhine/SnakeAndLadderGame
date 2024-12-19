using Microsoft.EntityFrameworkCore;
using SnakeandLadderGame.Database.Models;
using SnakeAndLadderGame.Domain.Models;
using SnakeAndLadderGame.Domain.Models.PlayGame;
using SnakeAndLadderGame.Domain.Models.StartGame;

namespace SnakeAndLadderGame.Domain.Features.Game;

public class GameService
{
    private readonly AppDbContext _context;

    public GameService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Result<StartGameResponseModel>> StartGameAsync(StartGameRequestModel request)
    {
        try
        {
            var board = await _context.TblBoards.FirstOrDefaultAsync(x => x.BoardId == request.BoardID);
            if (board is null)
            {
                return Result<StartGameResponseModel>.ValidationError("Invalid board ID.");
            }

            var newGame = new TblGame
            {
                GameId = Guid.NewGuid().ToString(),
                BoardId = request.BoardID,
                GameStatus = "InProgress"
            };

            await _context.TblGames.AddAsync(newGame);

            var colors = new List<string> { "Yellow", "Green", "Red", "Blue" };
            var shuffledColors = colors.OrderBy(x => Guid.NewGuid()).Take(request.PlayerIDs.Count()).ToList();

            for (int i = 0; i < request. PlayerIDs.Count; i++)
            {
                var gamePlayer = new TblGamePlayer
                {
                    GamePlayerId = Guid.NewGuid().ToString(),
                    GameId = newGame.GameId,
                    PlayerId = request.PlayerIDs[i],
                    Color = shuffledColors[i],
                    CurrentPosition = 1,
                    PlayerStatus = "Active"
                };
                await _context.TblGamePlayers.AddAsync(gamePlayer);
            }

            await _context.SaveChangesAsync();

            var response = new StartGameResponseModel
            {
                GameID = newGame.GameId,
                BoardID = newGame.BoardId,
                Players = request.PlayerIDs.Zip(shuffledColors, (id, color) => new PlayerDetailModel
                {
                    PlayerID = id,
                    Color = color,
                    CurrentPosition = 1
                }).ToList()
            };

            return Result<StartGameResponseModel>.Success(response, "Game started successfully.");
        }
        catch (Exception ex)
        {
            return Result<StartGameResponseModel>.SystemError("An error occurred while starting the game: " + ex.Message);
        }
    }
   

}
