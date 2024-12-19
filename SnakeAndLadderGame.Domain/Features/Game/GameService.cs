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

    #region StartGameAsync

    public async Task<Result<StartGameResponseModel>> StartGameAsync(StartGameRequestModel request)
    {
        try
        {
            var board = await _context.TblBoards.FirstOrDefaultAsync(x => x.BoardId == request.BoardID);
            if (board is null)
            {
                return Result<StartGameResponseModel>.ValidationError("Invalid board ID.");
            }

            #region Add New Game

            var newGame = new TblGame
            {
                GameId = Guid.NewGuid().ToString(),
                BoardId = request.BoardID,
                GameStatus = "InProgress"
            };

            await _context.TblGames.AddAsync(newGame);

            #endregion

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

    #endregion

    #region PlayGameAsync

    public async Task<Result<PlayGameResponseModel>> PlayGameAsync(PlayGameRequestModel request)
    {
        try
        {
            var game = await _context.TblGames
                .FirstOrDefaultAsync(g => g.GameId == request.GameID && g.GameStatus == "InProgress");

            if (game == null)
            {
                return Result<PlayGameResponseModel>.ValidationError("Invalid game ID or the game is not in progress.");
            }

            var gamePlayer = await _context.TblGamePlayers
                .FirstOrDefaultAsync(gp => gp.GameId == request.GameID && gp.PlayerId == request.PlayerID);

            if (gamePlayer == null)
            {
                return Result<PlayGameResponseModel>.ValidationError("Player is not part of this game.");
            }

            if (gamePlayer.PlayerStatus == "Won")
            {
                return Result<PlayGameResponseModel>.ValidationError("This player has already won.");
            }

            var diceRoll = new Random().Next(1, 7);
            var newPosition = gamePlayer.CurrentPosition + diceRoll;

            var board = await _context.TblBoards.FirstOrDefaultAsync(b => b.BoardId == game.BoardId);
            if (board == null)
            {
                return Result<PlayGameResponseModel>.SystemError("Board does not exist.");
            }

            TblCell? cell = null; 

            if (newPosition >= board.Size)
            {
                newPosition = board.Size;
                gamePlayer.PlayerStatus = "Won";

                var winnerRank = await _context.TblGameWinners.CountAsync(w => w.GameId == request.GameID) + 1;
                await _context.TblGameWinners.AddAsync(new TblGameWinner
                {
                    GameWinnerId = Guid.NewGuid().ToString(),
                    GameId = request.GameID,
                    PlayerId = request.PlayerID,
                    Rank = winnerRank
                });
            }
            else
            {
                cell = await _context.TblCells
                    .FirstOrDefaultAsync(c => c.BoardId == game.BoardId && c.CellNo == newPosition);

                if (cell != null)
                {
                    if (cell.CellType == "Snake")
                    {
                        newPosition = cell.DestinationCell!.Value;
                    }
                    else if (cell.CellType == "Ladder")
                    {
                        newPosition = cell.DestinationCell!.Value;
                    }
                }
            }

            var previousPosition = gamePlayer.CurrentPosition;
            gamePlayer.CurrentPosition = newPosition;

            await _context.SaveChangesAsync();

            var totalPlayers = await _context.TblGamePlayers.CountAsync(gp => gp.GameId == request.GameID);
            var winners = await _context.TblGameWinners.CountAsync(w => w.GameId == request.GameID);

            if (winners == totalPlayers - 1)
            {
                game.GameStatus = "Completed";
                await _context.SaveChangesAsync();
            }

            string responseMessage;
            if (gamePlayer.PlayerStatus == "Won")
            {
                responseMessage = "Congratulations! You won the game!";
            }
            else if (previousPosition < newPosition && cell != null && cell.CellType == "Ladder")
            {
                responseMessage = "You climbed a ladder!";
            }
            else if (previousPosition > newPosition && cell != null && cell.CellType == "Snake")
            {
                responseMessage = "You were bitten by a snake!";
            }
            else
            {
                responseMessage = "You moved forward.";
            }

            var response = new PlayGameResponseModel
            {
                PlayerID = gamePlayer.PlayerId,
                Color = gamePlayer.Color,
                DiceRoll = diceRoll,
                PreviousPosition = previousPosition,
                NewPosition = newPosition,
                Status = gamePlayer.PlayerStatus,
                Message = responseMessage
            };

            return Result<PlayGameResponseModel>.Success(response, "Move processed successfully.");
        }
        catch (Exception ex)
        {
            return Result<PlayGameResponseModel>.SystemError("An error occurred while processing the move: " + ex.Message);
        }
    }

    #endregion

}
