using IdleSkiller.Core.Events;

namespace IdleSkiller.Systems.Progress
{
    /// <summary>
    /// Represents a job that progresses over time until completion.
    /// </summary>
    public class ProgressJob
    {
        public ProgressJob(float duration)
        {
            Duration = duration;
            SpeedMultiplier = 1f;
        }

        /// <summary>
        /// Total duration required to complete the job.
        /// </summary>
        public float Duration { get; }

        /// <summary>
        /// Speed multiplier applied when ticking progress.
        /// </summary>
        public float SpeedMultiplier { get; set; }

        /// <summary>
        /// Chance [0,1] that the job completes with a critical success.
        /// </summary>
        public float CritChance { get; set; }

        /// <summary>
        /// Elapsed time accrued on this job.
        /// </summary>
        public float Elapsed { get; internal set; }

        /// <summary>
        /// Whether the job has completed.
        /// </summary>
        public bool IsComplete { get; internal set; }

        /// <summary>
        /// Whether the job completed with a critical success.
        /// </summary>
        public bool IsCritical { get; internal set; }
    }

    /// <summary>
    /// Event published when a job starts.
    /// </summary>
    public readonly struct JobStarted
    {
        public JobStarted(ProgressJob job)
        {
            Job = job;
        }

        public ProgressJob Job { get; }
    }

    /// <summary>
    /// Event published when a job completes.
    /// </summary>
    public readonly struct JobCompleted
    {
        public JobCompleted(ProgressJob job, bool isCritical)
        {
            Job = job;
            IsCritical = isCritical;
        }

        public ProgressJob Job { get; }
        public bool IsCritical { get; }
    }
}
