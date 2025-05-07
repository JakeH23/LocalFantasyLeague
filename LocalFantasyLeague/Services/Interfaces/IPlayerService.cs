using LocalFantasyLeague.Models.DbSets;

namespace LocalFantasyLeague.Services.Interfaces;

public interface IPlayerService
{
    Task<List<Player>> GetPlayerListIncludingTeam();
    Task<List<Player>> GetPlayerListIncludingStatsAndTeam();
    Task<List<Player>> GetPlayerListFilteredByPlayerIdList(List<int> playerIds);
    Task<List<Player>> GetPlayerListIncludingTeamFilteredByMatchId(int matchId);
    Task<List<Player>> GetPlayerListIncludingTeamFilteredByTeamIds(int homeTeamId, int awayTeamId);
    Task<Player?> GetPlayerIncludingTeamAndStatsFilteringByPlayerId(int playerId);
    Task<Player?> GetPlayerIncludingTeamAndStatDetailsAcrossMatchAndSeason(int playerId);
}