using System;

namespace IdleSkiller.Core.Events
{
    /// <summary>
    /// Simple publish/subscribe event bus.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Subscribe to events of type <typeparamref name="T"/>.
        /// </summary>
        void Subscribe<T>(Action<T> handler) where T : struct;

        /// <summary>
        /// Unsubscribe from events of type <typeparamref name="T"/>.
        /// </summary>
        void Unsubscribe<T>(Action<T> handler) where T : struct;

        /// <summary>
        /// Publish an event to all subscribers.
        /// </summary>
        void Publish<T>(in T eventData) where T : struct;
    }
}
