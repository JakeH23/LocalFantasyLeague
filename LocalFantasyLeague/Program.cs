using LocalFantasyLeague.Components;
using LocalFantasyLeague.Data;
using LocalFantasyLeague.Models;
using LocalFantasyLeague.Services;
using LocalFantasyLeague.Services.Components;
using LocalFantasyLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<LocalFantasyLeagueContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalFantasyLeagueContext") ?? throw new InvalidOperationException("Connection string 'LocalFantasyLeagueContext' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient(typeof(IComponentService<>), typeof(ComponentService<>));
builder.Services.AddTransient(typeof(ILogger<>), typeof(Logger<>));

builder.Services.AddTransient<IDataService, DataService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IMatchService, MatchService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<ISeasonService, SeasonService>();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<IPerformanceStatService, PerformanceStatService>();
builder.Services.AddTransient<IUserFantasySelectionService, UserFantasySelectionService>();
builder.Services.AddSingleton<UserSession>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
