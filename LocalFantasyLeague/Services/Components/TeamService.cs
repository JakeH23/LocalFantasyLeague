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
    }
}