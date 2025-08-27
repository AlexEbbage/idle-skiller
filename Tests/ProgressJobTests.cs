using System.Collections.Generic;
using IdleSkiller.Core.Events;
using IdleSkiller.Systems.Progress;
using NUnit.Framework;

namespace Tests
{
    public class ProgressJobTests
    {
        private EventBus _bus;
        private JobRunner _runner;
        private List<JobStarted> _started;
        private List<JobCompleted> _completed;

        [SetUp]
        public void Setup()
        {
            _bus = new EventBus();
            _runner = new JobRunner(_bus);
            _started = new List<JobStarted>();
            _completed = new List<JobCompleted>();
            _bus.Subscribe<JobStarted>(e => _started.Add(e));
            _bus.Subscribe<JobCompleted>(e => _completed.Add(e));
        }

        [Test]
        public void Tick_CompletesJob_And_FiresEvents()
        {
            var job = new ProgressJob(5f) { SpeedMultiplier = 1f };
            _runner.StartJob(job);
            for (int i = 0; i < 5; i++)
            {
                _runner.Tick(1f);
            }

            Assert.AreEqual(1, _started.Count);
            Assert.AreEqual(1, _completed.Count);
            Assert.True(job.IsComplete);
            Assert.AreEqual(0, _runner.ActiveJobCount);
        }

        [Test]
        public void Crit_Succeeds_When_ChanceIsCertain()
        {
            var job = new ProgressJob(1f) { CritChance = 1f };
            _runner.StartJob(job);
            _runner.Tick(1f);

            Assert.AreEqual(1, _completed.Count);
            Assert.True(_completed[0].IsCritical);
            Assert.True(job.IsCritical);
        }
    }
}
