using LocalFantasyLeague.Models;
using LocalFantasyLeague.Models.DbSets;

namespace LocalFantasyLeague.Services.Interfaces;

public interface IPerformanceStatService
{
    Task<List<PerformanceStat>> GetPerformanceStatListFilteredByMatchAndSelectedPlayers(int matchId, List<Player> selectedPlayers);
    Task<List<PerformanceStat>> GetPerformanceStatListFilteredByMatchIds(List<int> matchIds);
    Task<PerformanceStat?> GetPerformanceStatFilteredByPlayerId(int playerId);
    Task<List<FantasyScorer>> GetTopThreeFantasyPointScorers();
    Task<List<FantasyScorer>> GetTopThreeFantasyPointScorersByTeamId(int teamId);
    Task<List<FantasyScorer>> GetTopThreeFantasyPointScorersForCurrentSeason(int seasonId);
    Task<List<FantasyScorer>> GetTopThreeFantasyPointScorersByTeamIdAndSeasonId(int teamId, int seasonId);
}