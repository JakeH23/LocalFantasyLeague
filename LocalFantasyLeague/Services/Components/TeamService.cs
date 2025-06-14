using LocalFantasyLeague.Models.DbSets;
using LocalFantasyLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalFantasyLeague.Services.Components
{
    public class TeamService(IComponentService<TeamService> componentService) : ITeamService
    {
        private readonly IComponentService<TeamService> _componentService = componentService ?? throw new ArgumentNullException(nameof(componentService));

        public async Task<Team?> GetTeamIncludingPlayersAndTheirStats(int teamId)
        {
            return await _componentService.ExecuteQuery(async context =>
                    await context.Teams
                        .AsNoTracking()
                        .Include(t => t.Players)
                        .ThenInclude(p => p.PerformanceStats)
                        .ThenInclude(m => m.Match)
                        .FirstOrDefaultAsync(t => t.Id == teamId),
                $"Error fetching team including players and their stats for team ID: {teamId}.");
        }

        public async Task<Team?> GetCurrentUserTeamForSeason(int seasonId, int? teamId)
        {
            // Get the user's team
            var team = await _componentService.ExecuteQuery(async context =>
            await context.Teams
                .Include(t => t.Users)
                .FirstOrDefaultAsync(t => t.Id == teamId),
                $"Error fetching team including users by team ID: {teamId}.");

            // Check if this team participates in the season (e.g., via matches)
            bool teamInSeason = await _componentService.ExecuteQuery(async context =>
            await context.Matches
                .AnyAsync(m => m.SeasonId == seasonId && (m.HomeTeamId == team.Id || m.AwayTeamId == team.Id)),
                $"Error fetching matches with given season id: {seasonId}.");

            return teamInSeason ? team : null;
        }
    }
}