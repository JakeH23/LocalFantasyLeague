using LocalFantasyLeague.Models.DbSets;
using LocalFantasyLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalFantasyLeague.Services.Components
{
    public class MatchService(IComponentService<MatchService> componentService) : IMatchService
    {
        private readonly IComponentService<MatchService> _componentService = componentService ?? throw new ArgumentNullException(nameof(componentService));

        public async Task<List<Match>> GetMatchListIncludingTeamsAndSeason()
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Include(m => m.Season)
                    .OrderByDescending(m => m.Kickoff)
                    .ToListAsync(),
                "Error fetching matches including teams and season.");
        }

        public async Task<List<Match>> GetMatchListIncludingTeamsAndStats()
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Include(m => m.Stats)
                    .ToListAsync(),
                "Error fetching matches including teams and stats.");
        }

        public async Task<List<Match>> GetMatchListIncludingTeamsForCurrentUserTeamId(int teamId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Where(x => x.HomeTeamId == teamId || x.AwayTeamId == teamId)
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .OrderByDescending(m => m.Kickoff)
                    .ToListAsync(),
                $"Error fetching matches for team ID: {teamId}");
        }

        public async Task<List<Match>> GetMatchListIncludingTeamsFilteredBySeasonId(int seasonId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Where(m => m.SeasonId == seasonId)
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .OrderBy(m => m.Kickoff)
                    .ToListAsync(),
                $"Error fetching matches for season ID: {seasonId}");
        }

        public async Task<List<Match>> GetMatchListIncludingSeasonsFilteredByTeamId(int teamId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                    .Include(m => m.Season)
                    .ToListAsync(),
                $"Error fetching matches for team ID: {teamId}");
        }

        public async Task<List<int>> GetMatchIdListFilteredBySeasonId(int seasonId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Where(m => m.SeasonId == seasonId)
                    .Select(m => m.Id)
                    .ToListAsync(),
                $"Error fetching match IDs for season ID: {seasonId}");
        }

        public async Task<Match?> GetMatchIncludingTeamsByMatchId(int matchId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .FirstOrDefaultAsync(m => m.Id == matchId),
                $"Error fetching match for match ID: {matchId}");
        }

        public async Task<Match?> GetMatchIncludingTeamsAndPlayersAndStatsByMatchId(int matchId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam).ThenInclude(t => t.Players)
                    .Include(m => m.AwayTeam).ThenInclude(t => t.Players)
                    .Include(m => m.Stats)
                    .FirstOrDefaultAsync(m => m.Id == matchId),
                $"Error fetching match with players and stats for match ID: {matchId}");
        }

        public async Task<List<Match>> GetPreviousFiveMatches()
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Where(m => m.Kickoff < DateTime.Now)
                    .OrderByDescending(m => m.Kickoff)
                    .Take(5)
                    .ToListAsync(),
                "Error fetching previous five matches.");
        }

        public async Task<List<Match>> GetNextFiveMatches()
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Where(m => m.Kickoff > DateTime.Now)
                    .OrderBy(m => m.Kickoff)
                    .Take(5)
                    .ToListAsync(),
                "Error fetching next five matches.");
        }

        public async Task<List<Match>> GetPreviousFiveMatchesByTeamId(int teamId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Where(m => m.Kickoff < DateTime.Now && (m.HomeTeamId == teamId || m.AwayTeamId == teamId))
                    .OrderByDescending(m => m.Kickoff)
                    .Take(5)
                    .ToListAsync(),
                $"Error fetching previous five matches for team ID: {teamId}");
        }

        public async Task<List<Match>> GetNextFiveMatchesByTeamId(int teamId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Where(m => m.Kickoff > DateTime.Now && (m.HomeTeamId == teamId || m.AwayTeamId == teamId))
                    .OrderBy(m => m.Kickoff)
                    .Take(5)
                    .ToListAsync(),
                $"Error fetching next five matches for team ID: {teamId}");
        }

        public async Task<List<Match>> GetPreviousFiveMatchesForCurrentSeason(int seasonId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Where(m => m.SeasonId == seasonId && m.Kickoff < DateTime.Now)
                    .OrderByDescending(m => m.Kickoff)
                    .Take(5)
                    .ToListAsync(),
                $"Error fetching previous five matches for season ID: {seasonId}");
        }

        public async Task<List<Match>> GetNextFiveMatchesForCurrentSeason(int seasonId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Where(m => m.SeasonId == seasonId && m.Kickoff > DateTime.Now)
                    .OrderBy(m => m.Kickoff)
                    .Take(5)
                    .ToListAsync(),
                $"Error fetching next five matches for season ID: {seasonId}");
        }

        public async Task<List<Match>> GetMatchListRequiringStatsToBeEntered()
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Include(m => m.Stats)
                    .Where(m => m.Kickoff < DateTime.Now && !m.Stats.Any())
                    .ToListAsync(),
                "Error fetching matches requiring stats to be entered.");
        }

        public async Task<List<Match>> GetMatchListForNextSevenDaysRequiringFantasyPicks(List<int> matchIds)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Where(m => m.Kickoff >= DateTime.Now && m.Kickoff <= DateTime.Now.AddDays(7))
                    .Where(m => !matchIds.Contains(m.Id))
                    .ToListAsync(),
                "Error fetching matches for the next seven days requiring fantasy picks.");
        }

        public async Task<List<Match>> GetPreviousFiveMatchesByTeamIdAndSeasonId(int teamId, int seasonId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Where(m => m.Kickoff < DateTime.Now
                         && m.SeasonId == seasonId
                         && (m.HomeTeamId == teamId || m.AwayTeamId == teamId))
                    .OrderByDescending(m => m.Kickoff)
                    .Take(5)
                    .ToListAsync(),
                $"Error fetching previous five matches for team ID: {teamId}");
        }

        public async Task<List<Match>> GetNextFiveMatchesByTeamIdAndSeasonId(int teamId, int seasonId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Matches
                    .AsNoTracking()
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Where(m => m.Kickoff > DateTime.Now
                        && m.SeasonId == seasonId
                        && (m.HomeTeamId == teamId || m.AwayTeamId == teamId))
                    .OrderBy(m => m.Kickoff)
                    .Take(5)
                    .ToListAsync(),
                $"Error fetching next five matches for team ID: {teamId}");
        }
    }
}
