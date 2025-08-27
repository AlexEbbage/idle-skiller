using System;

namespace IdleSkiller.Core.Time
{
    /// <summary>
    /// Abstraction over time retrieval to allow deterministic testing.
    /// </summary>
    public interface ITimeService
    {
        /// <summary>
        /// Gets the current UTC time.
        /// </summary>
        DateTime UtcNow { get; }
    }
}
