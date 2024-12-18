using SnakeandLadderGame.Database.Models;

namespace SnakeAndLadderGame.Domain.Features.Game;

public class GameService
{
    private readonly AppDbContext _context;

    public GameService(AppDbContext context)
    {
        _context = context;
    }
}
