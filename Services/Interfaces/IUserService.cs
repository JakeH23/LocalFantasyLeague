using LocalFantasyLeague.Models.DbSets;

namespace LocalFantasyLeague.Services.Interfaces;

public interface IUserService
{
    Task<User> GetCurrentUserAsync(int userId);
    Task<User?> GetUserLoginAsync(string username, string password);
    Task<bool> UserExistsAsync(string username);
    string ComputeSha256Hash(string rawData);
}