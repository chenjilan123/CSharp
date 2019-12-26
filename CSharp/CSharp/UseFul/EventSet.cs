using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSharp.UseFul
{
    public sealed class EventKey { }
    public sealed class EventSet
    {
        private readonly Dictionary<EventKey, Delegate> _events = new Dictionary<EventKey, Delegate>();

        public void Add(EventKey key, Delegate handler)
        {
            Monitor.Enter(_events);
            _events.TryGetValue(key, out var d);
            _events[key] = Delegate.Combine(d, handler);
            Monitor.Exit(_events);
        }

        public void Remove(EventKey key, Delegate handler)
        {
            Monitor.Enter(_events);
            if (_events.TryGetValue(key, out var d))
            {
                d = Delegate.Remove(d, handler);
                if (d != null) _events[key] = d;
                else _events.Remove(key);
            }
            Monitor.Exit(_events);
        }

        public void Raise(EventKey key, object sender, EventArgs e)
        {
            Monitor.Enter(_events);
            _events.TryGetValue(key, out var d);
            Monitor.Exit(_events);
            if (d != null)
            {
                d.DynamicInvoke(sender, e);
            }
        }
    }
}
