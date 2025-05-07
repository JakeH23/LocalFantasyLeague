using LocalFantasyLeague.Data;
using LocalFantasyLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace LocalFantasyLeague.Services
{
    public class DeleteService(IDbContextFactory<LocalFantasyLeagueContext> dbContextFactory) : IDeleteService
    {
        public async Task DeleteAllCorrespondingMatchData(int matchId)
        {
            using var context = dbContextFactory.CreateDbContext();

            // Delete related PerformanceStats
            var performanceStats = context.PerformanceStats.Where(ps => ps.MatchId == matchId);
            context.PerformanceStats.RemoveRange(performanceStats);

            // Delete related UserFantasySelections
            var userFantasySelections = context.UserFantasySelections.Where(ufs => ufs.MatchId == matchId);
            context.UserFantasySelections.RemoveRange(userFantasySelections);

            // Save changes to the database
            await context.SaveChangesAsync();
        }
    }
}
