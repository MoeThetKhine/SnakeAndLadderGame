﻿namespace SnakeAndLadderGame.Domain.Features.Player;

public class PlayerService
{
    private readonly AppDbContext _context;

    public PlayerService(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    #region CreatePlayerAsync

    public async Task<Result<PlayerRequestModel>> CreatePlayerAsync(PlayerRequestModel request)
    {
        try
        {
            #region Find Last Player

            var lastPlayer = await _context.TblPlayers
                .OrderByDescending(p => p.PlayerId)
                .FirstOrDefaultAsync();

            #endregion

            #region Create Player Id

            int nextId = 1;
            if (lastPlayer is not null && lastPlayer.PlayerId.StartsWith("p"))
            {
                int.Parse(lastPlayer.PlayerId.Substring(1));
                nextId++;
            }

            string newPlayerId = $"p{nextId}";

            #endregion

            #region Validation for Player Name and Email

            if (string.IsNullOrEmpty(request.PlayerName)) 
            {
                return Result<PlayerRequestModel>.ValidationError("Player Name cannot be empty ");
            }

            if(string.IsNullOrEmpty(request.Email))
            {
                return Result<PlayerRequestModel>.ValidationError("Email cannot be empty ");
            }

            #endregion

            #region Create New Player

            var newPlayer = new TblPlayer
            {
                PlayerId = newPlayerId,
                PlayerName = request.PlayerName,
                Email = request.Email
            };

            await _context.TblPlayers.AddAsync(newPlayer);
            await _context.SaveChangesAsync();

            #endregion

            return Result<PlayerRequestModel>.Success(request);
        }
        catch (Exception ex)
        {
            return Result<PlayerRequestModel>.SystemError("An error occurred while creating the player: " + ex.Message);
        }
    }

    #endregion
}
