using LocalFantasyLeague.Models.DbSets;

namespace LocalFantasyLeague.Services.Interfaces;

public interface IMatchService
{
    Task<List<Match>> GetMatchListIncludingTeamsAndSeason();
    Task<List<Match>> GetMatchListIncludingTeamsAndStats();
    Task<List<Match>> GetMatchListIncludingTeamsForCurrentUserTeamId(int teamId);
    Task<List<Match>> GetMatchListIncludingTeamsFilteredBySeasonId(int seasonId);
    Task<List<Match>> GetMatchListIncludingSeasonsFilteredByTeamId(int teamId);
    Task<List<int>> GetMatchIdListFilteredBySeasonId(int seasonId);
    Task<Match?> GetMatchIncludingTeamsByMatchId(int matchId);
    Task<Match?> GetMatchIncludingTeamsAndPlayersAndStatsByMatchId(int matchId);
    Task<List<Match>> GetPreviousFiveMatches();
    Task<List<Match>> GetNextFiveMatches();
    Task<List<Match>> GetPreviousFiveMatchesByTeamId(int teamId);
    Task<List<Match>> GetNextFiveMatchesByTeamId(int teamId);
    Task<List<Match>> GetPreviousFiveMatchesForCurrentSeason(int seasonId);
    Task<List<Match>> GetNextFiveMatchesForCurrentSeason(int seasonId);
    Task<List<Match>> GetMatchListRequiringStatsToBeEntered();
    Task<List<Match>> GetMatchListForNextSevenDaysRequiringFantasyPicks(List<int> matchIds);
    Task<List<Match>> GetPreviousFiveMatchesByTeamIdAndSeasonId(int teamId, int seasonId);
    Task<List<Match>> GetNextFiveMatchesByTeamIdAndSeasonId(int teamId, int seasonId);

}