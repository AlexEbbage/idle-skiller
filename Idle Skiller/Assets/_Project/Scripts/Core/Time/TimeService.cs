using System;

namespace IdleSkiller.Core.Time
{
    /// <summary>
    /// Default implementation of <see cref="ITimeService"/> using <see cref="DateTime.UtcNow"/>.
    /// </summary>
    public class TimeService : ITimeService
    {
        /// <inheritdoc />
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
