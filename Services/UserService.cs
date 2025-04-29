using LocalFantasyLeague.Data;
using LocalFantasyLeague.Models.DbSets;
using LocalFantasyLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LocalFantasyLeague.Services;

public class UserService(IDbContextFactory<LocalFantasyLeagueContext> dbContextFactory) : IUserService
{
    public async Task<User> GetCurrentUserAsync(int userId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? new User();
    }

    public async Task<User?> GetUserLoginAsync(string username, string password)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        var hash = ComputeSha256Hash(password);
        return await context.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == hash);
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Users.AnyAsync(u => u.Username == username);
    }

    public string ComputeSha256Hash(string rawData)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        return Convert.ToBase64String(bytes);
    }
}