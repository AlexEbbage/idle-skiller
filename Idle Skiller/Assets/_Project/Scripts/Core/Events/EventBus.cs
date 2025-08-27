using System;
using System.Collections.Generic;

namespace IdleSkiller.Core.Events
{
    /// <summary>
    /// Basic in-memory implementation of <see cref="IEventBus"/>.
    /// </summary>
    public class EventBus : IEventBus
    {
        private class EventStream<T> where T : struct
        {
            private readonly List<Action<T>> _handlers = new();
            private Action<T>[] _invokeList = Array.Empty<Action<T>>();
            private bool _dirty = true;

            public void Subscribe(Action<T> handler)
            {
                _handlers.Add(handler);
                _dirty = true;
            }

            public void Unsubscribe(Action<T> handler)
            {
                if (_handlers.Remove(handler))
                {
                    _dirty = true;
                }
            }

            public void Publish(in T eventData)
            {
                if (_dirty)
                {
                    _invokeList = _handlers.ToArray();
                    _dirty = false;
                }

                for (int i = 0; i < _invokeList.Length; i++)
                {
                    _invokeList[i]?.Invoke(eventData);
                }
            }
        }

        private readonly Dictionary<Type, object> _streams = new();

        private EventStream<T> GetStream<T>() where T : struct
        {
            var type = typeof(T);
            if (!_streams.TryGetValue(type, out var stream))
            {
                stream = new EventStream<T>();
                _streams[type] = stream;
            }

            return (EventStream<T>)stream;
        }

        public void Subscribe<T>(Action<T> handler) where T : struct => GetStream<T>().Subscribe(handler);

        public void Unsubscribe<T>(Action<T> handler) where T : struct => GetStream<T>().Unsubscribe(handler);

        public void Publish<T>(in T eventData) where T : struct => GetStream<T>().Publish(eventData);
    }
}
