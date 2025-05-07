using LocalFantasyLeague.Models.DbSets;

namespace LocalFantasyLeague.Models
{
    public class SeasonStats
    {
        public Season Season { get; set; } = null!;
        public List<PerformanceStat> Stats { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        public int TotalPages => Math.Max(1, (int)Math.Ceiling((double)Stats.Count / PageSize));
        public bool IsFirstPage => CurrentPage == 1;
        public bool IsLastPage => CurrentPage >= TotalPages;

        public IEnumerable<PerformanceStat> PaginatedStats =>
            Stats
                .OrderByDescending(s => s.Match?.Kickoff)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize);

        public int TotalAppearances => Stats.Count(s => s.Appearance);
        public int TotalGoals => Stats.Sum(s => s.Goals);
        public int TotalAssists => Stats.Sum(s => s.Assists);
        public int TotalYellowCards => Stats.Count(s => s.YellowCard);
        public int TotalRedCards => Stats.Count(s => s.RedCard);
        public int TotalPenaltySaves => Stats.Sum(s => s.PenaltySaves);
        public int TotalCleanSheets => Stats.Count(s => s.CleanSheet);
        public int TotalPenaltyMissed => Stats.Sum(s => s.PenaltyMissed);
    }
}
