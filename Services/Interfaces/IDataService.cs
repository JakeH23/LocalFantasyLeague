using LocalFantasyLeague.Models.DbSets;

namespace LocalFantasyLeague.Services.Interfaces;

public interface IDataService
{
    Task<List<T>> GetAllAsync<T>() where T : class;
    Task<T?> GetByIdAsync<T>(int id) where T : class, IEntity;
    Task<T?> FindAsync<T>(int id) where T : class;
    Task UpdateAsync<T>(T entity) where T : class;
    Task AddAsync<T>(T entity) where T : class;
    Task RemoveAsync<T>(T entity) where T : class;
}