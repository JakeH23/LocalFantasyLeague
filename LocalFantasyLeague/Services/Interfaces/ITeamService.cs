using LocalFantasyLeague.Models.DbSets;

namespace LocalFantasyLeague.Services.Interfaces;

public interface ITeamService
{
    Task<Team?> GetTeamIncludingPlayersAndTheirStats(int teamId);
    Task<Team?> GetCurrentUserTeamForSeason(int seasonId, int? teamId);
}