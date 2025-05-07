using LocalFantasyLeague.Models.DbSets;
using LocalFantasyLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalFantasyLeague.Services.Components
{
    public class PlayerService(IComponentService<PlayerService> componentService) : IPlayerService
    {
        private readonly IComponentService<PlayerService> _componentService = componentService ?? throw new ArgumentNullException(nameof(componentService));

        public async Task<List<Player>> GetPlayerListIncludingTeam()
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Players
                    .AsNoTracking()
                    .Include(p => p.Team)
                    .ToListAsync(),
                "Error fetching player list including team.");
        }

        public async Task<List<Player>> GetPlayerListIncludingStatsAndTeam()
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Players
                    .AsNoTracking()
                    .Include(p => p.PerformanceStats)
                    .Include(p => p.Team)
                    .ToListAsync(),
                "Error fetching player list including stats and team.");
        }

        public async Task<List<Player>> GetPlayerListFilteredByPlayerIdList(List<int> playerIds)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Players
                    .AsNoTracking()
                    .Where(p => playerIds.Contains(p.Id))
                    .ToListAsync(),
                "Error fetching player list filtered by player IDs.");
        }

        public async Task<List<Player>> GetPlayerListIncludingTeamFilteredByMatchId(int matchId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Players
                    .AsNoTracking()
                    .Include(p => p.Team)
                    .Where(p => p.TeamId == context.Matches.FirstOrDefault(m => m.Id == matchId).HomeTeamId ||
                                p.TeamId == context.Matches.FirstOrDefault(m => m.Id == matchId).AwayTeamId)
                    .ToListAsync(),
                $"Error fetching player list including team filtered by match ID: {matchId}.");
        }

        public async Task<List<Player>> GetPlayerListIncludingTeamFilteredByTeamIds(int homeTeamId, int awayTeamId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Players
                    .AsNoTracking()
                    .Include(p => p.Team)
                    .Where(p => p.TeamId == homeTeamId || p.TeamId == awayTeamId)
                    .ToListAsync(),
                $"Error fetching player list including team filtered by team IDs: {homeTeamId}, {awayTeamId}.");
        }

        public async Task<Player?> GetPlayerIncludingTeamAndStatsFilteringByPlayerId(int playerId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Players
                    .AsNoTracking()
                    .Include(p => p.PerformanceStats)
                    .Include(p => p.Team)
                    .FirstOrDefaultAsync(p => p.Id == playerId),
                $"Error fetching player including team and stats filtered by player ID: {playerId}.");
        }

        public async Task<Player?> GetPlayerIncludingTeamAndStatDetailsAcrossMatchAndSeason(int playerId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.Players
                    .AsNoTracking()
                    .Include(p => p.Team)
                    .Include(p => p.PerformanceStats)
                    .ThenInclude(ps => ps.Match)
                    .ThenInclude(m => m.Season)
                    .Include(p => p.PerformanceStats)
                    .ThenInclude(ps => ps.Match)
                    .ThenInclude(m => m.HomeTeam)
                    .Include(p => p.PerformanceStats)
                    .ThenInclude(ps => ps.Match)
                    .ThenInclude(m => m.AwayTeam)
                    .FirstOrDefaultAsync(p => p.Id == playerId),
                $"Error fetching player including team and stat details across match and season for player ID: {playerId}.");
        }
    }
}
