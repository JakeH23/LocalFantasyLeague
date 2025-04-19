using LocalFantasyLeague.Models;

namespace LocalFantasyLeague.Services.Interfaces
{
    public interface IBettingService
    {
        Task PlaceBetAsync(Bet bet);
        Task<List<Bet>> GetBetsForUserAsync(string userId);
    }
}
