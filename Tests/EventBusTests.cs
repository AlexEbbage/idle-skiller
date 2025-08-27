using System;
using IdleSkiller.Core.Events;
using NUnit.Framework;

namespace IdleSkiller.Tests
{
    public class EventBusTests
    {
        [Test]
        public void PublishCallsSubscribers()
        {
            var bus = new EventBus();
            JobStarted received = default;
            bus.Subscribe<JobStarted>(e => received = e);

            var evt = new JobStarted("test");
            bus.Publish(evt);

            Assert.That(received.JobId, Is.EqualTo("test"));
        }

        [Test]
        public void UnsubscribeStopsReceiving()
        {
            var bus = new EventBus();
            int callCount = 0;
            Action<JobStarted> handler = _ => callCount++;

            bus.Subscribe(handler);
            bus.Unsubscribe(handler);
            bus.Publish(new JobStarted("job"));

            Assert.That(callCount, Is.EqualTo(0));
        }

        [Test]
        public void CanUnsubscribeDuringPublish()
        {
            var bus = new EventBus();
            int count = 0;
            Action<JobStarted> handler1 = null;
            handler1 = e =>
            {
                count++;
                bus.Unsubscribe(handler1);
            };
            Action<JobStarted> handler2 = e => count++;

            bus.Subscribe(handler1);
            bus.Subscribe(handler2);

            bus.Publish(new JobStarted("job"));
            Assert.That(count, Is.EqualTo(2));

            bus.Publish(new JobStarted("job"));
            Assert.That(count, Is.EqualTo(3));
        }
    }
}
