using LocalFantasyLeague.Models.DbSets;

namespace LocalFantasyLeague.Utilities
{
    public static class PointCalculator
    {
        /// <summary>
        /// Calculates the total points for a list of performance stats.
        /// </summary>
        /// <param name="stats">The list of performance stats.</param>
        /// <returns>The total points.</returns>
        public static int CalculateTotalPoints(List<PerformanceStat> stats)
        {
            return stats.Sum(stat =>
                (stat.Appearance ? 1 : 0) +
                (stat.Goals * 3) +
                (stat.Assists * 1) +
                (stat.YellowCard ? -2 : 0) +
                (stat.RedCard ? -5 : 0) +
                (stat.PenaltySaves * 1) +
                (stat.CleanSheet ? 5 : 0) +
                (stat.PenaltyMissed * -1)
            );
        }
    }
}
