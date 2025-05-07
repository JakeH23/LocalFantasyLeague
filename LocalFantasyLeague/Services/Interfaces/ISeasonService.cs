using LocalFantasyLeague.Models.DbSets;

namespace LocalFantasyLeague.Services.Interfaces;

public interface ISeasonService
{
    Task<List<Season>> GetSeasonListOrderedByDescendingStartDate();
    Task<Season?> GetSeasonBySeasonId(int seasonId);
}