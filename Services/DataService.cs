using LocalFantasyLeague.Data;
using LocalFantasyLeague.Models.DbSets;
using LocalFantasyLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalFantasyLeague.Services;

public class DataService(IDbContextFactory<LocalFantasyLeagueContext> dbContextFactory) : IDataService
{
    public async Task<List<T>> GetAllAsync<T>() where T : class
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync<T>(int id) where T : class, IEntity
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Set<T>().FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<T?> FindAsync<T>(int id) where T : class
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync<T>(T entity) where T : class
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task AddAsync<T>(T entity) where T : class
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        context.Set<T>().Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task RemoveAsync<T>(T entity) where T : class
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }
}