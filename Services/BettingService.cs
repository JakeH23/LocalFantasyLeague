using LocalFantasyLeague.Data;
using LocalFantasyLeague.Models;
using LocalFantasyLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalFantasyLeague.Services
{
    public class BettingService : IBettingService
    {
        private readonly LocalFantasyLeagueContext _context;

        public BettingService(LocalFantasyLeagueContext context)
        {
            _context = context;
        }

        public async Task PlaceBetAsync(Bet bet)
        {
            _context.Bets.Add(bet);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bet>> GetBetsForUserAsync(string userId)
        {
            return await _context.Bets
                .Where(b => b.UserId == userId)
                .Include(b => b.PlayerId)
                .ToListAsync();
        }
    }

}
