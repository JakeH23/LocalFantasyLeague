using LocalFantasyLeague.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalFantasyLeague.Services.Components;

public interface IComponentService<T>
{
    Task<T> ExecuteQuery<T>(Func<LocalFantasyLeagueContext, Task<T>> query, string errorMessage);
}

public class ComponentService<T>(IDbContextFactory<LocalFantasyLeagueContext> dbContextFactory, ILogger<T> logger) : IComponentService<T>
{
    private readonly IDbContextFactory<LocalFantasyLeagueContext> _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    private readonly ILogger<T> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<T> ExecuteQuery<T>(Func<LocalFantasyLeagueContext, Task<T>> query, string errorMessage)
    {
        try
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await query(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, errorMessage);
            throw;
        }
    }
}