using LocalFantasyLeague.Models.DbSets;
using LocalFantasyLeague.Utilities;
using Microsoft.EntityFrameworkCore;
using LocalFantasyLeague.Models;
using LocalFantasyLeague.Services.Interfaces;

namespace LocalFantasyLeague.Services.Components
{
    public class PerformanceStatService(IComponentService<PerformanceStatService> componentService) : IPerformanceStatService
    {
        private readonly IComponentService<PerformanceStatService> _componentService = componentService ?? throw new ArgumentNullException(nameof(componentService));

        public async Task<List<PerformanceStat>> GetPerformanceStatListFilteredByMatchAndSelectedPlayers(int matchId, List<Player> selectedPlayers)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.PerformanceStats
                    .AsNoTracking()
                    .Where(ps => ps.MatchId == matchId && selectedPlayers.Select(p => p.Id).Contains(ps.PlayerId))
                    .ToListAsync(),
                $"Error fetching performance stats for match ID: {matchId} and selected players.");
        }

        public async Task<List<PerformanceStat>> GetPerformanceStatListFilteredByMatchIds(List<int> matchIds)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.PerformanceStats
                    .AsNoTracking()
                    .Where(ps => matchIds.Contains(ps.MatchId))
                    .ToListAsync(),
                "Error fetching performance stats for the provided match IDs.");
        }

        public async Task<PerformanceStat?> GetPerformanceStatFilteredByPlayerId(int playerId)
        {
            return await _componentService.ExecuteQuery(async context =>
                await context.PerformanceStats
                    .AsNoTracking()
                    .Where(ps => ps.PlayerId == playerId)
                    .OrderByDescending(ps => ps.Match!.Kickoff)
                    .FirstOrDefaultAsync(),
                $"Error fetching performance stats for player ID: {playerId}.");
        }

        public async Task<List<FantasyScorer>> GetTopThreeFantasyPointScorers()
        {
            return await _componentService.ExecuteQuery(async context =>
                (await context.PerformanceStats
                        .AsNoTracking()
                        .Include(ps => ps.Player)
                        .GroupBy(ps => ps.PlayerId)
                        .Select(g => new { PlayerId = g.Key, Name = g.First().Player!.Name, PerformanceStats = g.Select(ps => ps).ToList() })
                        .ToListAsync()
                )
                .Select(g => new FantasyScorer
                {
                    PlayerId = g.PlayerId,
                    Name = g.Name,
                    FantasyPoints = PointCalculator.CalculateTotalPoints(g.PerformanceStats)
                })
                .OrderByDescending(fs => fs.FantasyPoints)
                .Take(3)
                .ToList(),
                "Error fetching top three fantasy point scorers.");
        }

        public async Task<List<FantasyScorer>> GetTopThreeFantasyPointScorersByTeamId(int teamId)
        {
            return await _componentService.ExecuteQuery(async context =>
                (await context.PerformanceStats
                        .AsNoTracking()
                        .Include(ps => ps.Player)
                        .Where(ps => ps.Player!.TeamId == teamId)
                        .GroupBy(ps => ps.PlayerId)
                        .Select(g => new { PlayerId = g.Key, Name = g.First().Player!.Name, PerformanceStats = g.ToList() })
                        .ToListAsync()
                )
                .Select(g => new FantasyScorer
                {
                    PlayerId = g.PlayerId,
                    Name = g.Name,
                    FantasyPoints = PointCalculator.CalculateTotalPoints(g.PerformanceStats)
                })
                .OrderByDescending(fs => fs.FantasyPoints)
                .Take(3)
                .ToList(),
                $"Error fetching top three fantasy point scorers for team ID: {teamId}.");
        }
    }
}
