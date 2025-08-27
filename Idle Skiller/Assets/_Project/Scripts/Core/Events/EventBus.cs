using System;
using System.Collections.Generic;

namespace IdleSkiller.Core.Events
{
    /// <summary>
    /// Basic in-memory implementation of <see cref="IEventBus"/>.
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        public void Subscribe<T>(Action<T> handler) where T : struct
        {
            var type = typeof(T);
            if (!_subscribers.TryGetValue(type, out var list))
            {
                list = new List<Delegate>();
                _subscribers[type] = list;
            }

            if (!list.Contains(handler))
            {
                list.Add(handler);
            }
        }

        public void Unsubscribe<T>(Action<T> handler) where T : struct
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var list))
            {
                list.Remove(handler);
            }
        }

        public void Publish<T>(T evt) where T : struct
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var list))
            {
                // iterate over copy to be safe against modifications during invoke
                var copy = list.ToArray();
                foreach (var del in copy)
                {
                    ((Action<T>)del)(evt);
                }
            }
        }
    }
}
