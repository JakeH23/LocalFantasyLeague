using LocalFantasyLeague.Models.DbSets;
using LocalFantasyLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalFantasyLeague.Services.Components
{
    public class SeasonService(IComponentService<SeasonService> componentService) : ISeasonService
    {
        private readonly IComponentService<SeasonService> _componentService = componentService ?? throw new ArgumentNullException(nameof(componentService));

        public async Task<List<Season>> GetSeasonListOrderedByDescendingStartDate()
        {
            return await _componentService.ExecuteQuery(async context =>
                    await context.Seasons
                        .AsNoTracking()
                        .OrderByDescending(s => s.StartDate)
                        .ToListAsync(),
                "Error fetching season list ordered by descending start date.");
        }

        public async Task<Season?> GetSeasonBySeasonId(int seasonId)
        {
            return await _componentService.ExecuteQuery(async context =>
                    await context.Seasons
                        .AsNoTracking()
                        .Where(s => s.Id == seasonId)
                        .FirstOrDefaultAsync(),
                $"Error fetching season by {seasonId}.");
        }
    }
}