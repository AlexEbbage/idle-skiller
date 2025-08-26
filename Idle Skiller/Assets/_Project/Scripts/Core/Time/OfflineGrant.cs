using System;

namespace IdleSkiller.Core.Time
{
    /// <summary>
    /// Provides helper methods for calculating offline progress.
    /// </summary>
    public static class OfflineGrant
    {
        /// <summary>
        /// Calculates the number of completed cycles that would have occurred
        /// during an offline period.
        /// </summary>
        /// <param name="elapsed">The elapsed offline time in seconds.</param>
        /// <param name="duration">The duration of a single cycle in seconds.</param>
        /// <param name="speedMultiplier">Speed multiplier applied to the process.</param>
        /// <returns>The number of fully completed cycles.</returns>
        public static int CompletedCycles(double elapsed, double duration, double speedMultiplier)
        {
            if (elapsed <= 0 || duration <= 0 || speedMultiplier <= 0)
            {
                return 0;
            }

            var cycles = elapsed * speedMultiplier / duration;
            return (int)Math.Floor(cycles);
        }
    }
}
