using LocalFantasyLeague.Models.DbSets;

namespace LocalFantasyLeague.Services.Interfaces;

public interface IUserFantasySelectionService
{
    Task<List<UserFantasySelection>> GetUserFantasySelectionListFilteredByUserId(int userId);
    Task<List<int>> GetUserFantasySelectionListOfMatchIdsFilteredByUserId(int userId);
}