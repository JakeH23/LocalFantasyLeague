using LocalFantasyLeague.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalFantasyLeague.Data
{
    public class LocalFantasyLeagueContext : DbContext
    {
        public LocalFantasyLeagueContext(DbContextOptions<LocalFantasyLeagueContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; } = default!;
        public DbSet<Team> Teams { get; set; } = default!;
        public DbSet<Match> Matches { get; set; } = default!;
        public DbSet<PerformanceStat> PerformanceStats { get; set; } = default!;
        public DbSet<Bet> Bets { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .Property(t => t.Name)
                .IsRequired(); // Enforce non-nullable column

            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany()
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany()
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            modelBuilder.Entity<PerformanceStat>()
                .HasOne(ps => ps.Match)
                .WithMany(m => m.Stats)
                .HasForeignKey(ps => ps.MatchId);

            modelBuilder.Entity<PerformanceStat>()
                .HasOne(ps => ps.Player)
                .WithMany(p => p.PerformanceStats)
                .HasForeignKey(ps => ps.PlayerId);

        }

    }
}
