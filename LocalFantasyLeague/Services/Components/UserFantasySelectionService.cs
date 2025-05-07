using LocalFantasyLeague.Models.DbSets;
using LocalFantasyLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalFantasyLeague.Services.Components
{
    public class UserFantasySelectionService(IComponentService<UserFantasySelectionService> componentService) : IUserFantasySelectionService
    {
        private readonly IComponentService<UserFantasySelectionService> _componentService = componentService ?? throw new ArgumentNullException(nameof(componentService));

        public async Task<List<UserFantasySelection>> GetUserFantasySelectionListFilteredByUserId(int userId)
        {
            return await _componentService.ExecuteQuery(async context =>
                    await context.UserFantasySelections
                        .AsNoTracking()
                        .Where(selection => selection.UserId == userId)
                        .ToListAsync(),
                $"Error fetching UserFantasySelections for UserId: {userId}.");
        }

        public async Task<List<int>> GetUserFantasySelectionListOfMatchIdsFilteredByUserId(int userId)
        {
            return await _componentService.ExecuteQuery(async context =>
                    await context.UserFantasySelections
                        .AsNoTracking()
                        .Where(s => s.UserId == userId)
                        .Select(s => s.MatchId)
                        .ToListAsync(),
                $"Error fetching MatchIds for UserId: {userId}.");
        }
    }
}