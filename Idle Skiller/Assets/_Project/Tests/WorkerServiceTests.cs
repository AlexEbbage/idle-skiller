using IdleSkiller.Systems;
using IdleSkiller.Systems.Workers;
using NUnit.Framework;

namespace IdleSkiller.Tests
{
    public class WorkerServiceTests
    {
        [Test]
        public void Constructor_AddsInitialWorker()
        {
            var save = new SaveData();
            var service = new WorkerService(save);

            Assert.That(service.Workers.Count, Is.EqualTo(1));
            var worker = service.Workers[0];
            Assert.That(worker.Name, Is.EqualTo("Worker"));
            Assert.That(worker.State, Is.EqualTo(WorkerState.Idle));
        }

        [Test]
        public void DerivedStats_FromSkillLevel()
        {
            var worker = WorkerFactory.CreateInitial();
            worker.SkillLevels["mining"] = 10;

            Assert.That(worker.GetSpeedPercent("mining"), Is.EqualTo(1.1f));
            Assert.That(worker.GetYieldPercent("mining"), Is.EqualTo(1.1f));
            Assert.That(worker.GetCritPercent("mining"), Is.EqualTo(0.1f));
            Assert.That(worker.GetXpPercent("mining"), Is.EqualTo(1.1f));
        }
    }
}
