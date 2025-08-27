using System;

namespace IdleSkiller.Core.Events
{
    public interface IEventBus
    {
        void Subscribe<T>(Action<T> handler) where T : struct;
        void Unsubscribe<T>(Action<T> handler) where T : struct;
        void Publish<T>(in T eventData) where T : struct;
    }
}
