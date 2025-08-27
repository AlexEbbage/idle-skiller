using System;
using System.Collections.Generic;
using IdleSkiller.Core.Events;

namespace IdleSkiller.Systems.Progress
{
    /// <summary>
    /// Updates active <see cref="ProgressJob"/> instances.
    /// </summary>
    public class JobRunner
    {
        private readonly IEventBus _eventBus;
        private readonly List<ProgressJob> _jobs = new();
        private readonly Random _random = new();

        public JobRunner(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        /// <summary>
        /// Number of active jobs.
        /// </summary>
        public int ActiveJobCount => _jobs.Count;

        /// <summary>
        /// Begin tracking and updating the provided job.
        /// </summary>
        public void StartJob(ProgressJob job)
        {
            if (!_jobs.Contains(job))
            {
                job.Elapsed = 0f;
                job.IsComplete = false;
                job.IsCritical = false;
                _jobs.Add(job);
                _eventBus.Publish(new JobStarted(job));
            }
        }

        /// <summary>
        /// Advance all jobs by the specified delta time.
        /// </summary>
        public void Tick(float deltaTime)
        {
            for (int i = _jobs.Count - 1; i >= 0; i--)
            {
                var job = _jobs[i];
                if (job.IsComplete)
                {
                    _jobs.RemoveAt(i);
                    continue;
                }

                job.Elapsed += deltaTime * job.SpeedMultiplier;
                if (job.Elapsed >= job.Duration)
                {
                    job.Elapsed = job.Duration;
                    job.IsComplete = true;
                    job.IsCritical = _random.NextDouble() < job.CritChance;
                    _jobs.RemoveAt(i);
                    _eventBus.Publish(new JobCompleted(job, job.IsCritical));
                }
            }
        }
    }
}
