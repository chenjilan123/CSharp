using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Model
{
    public class DynamicDelegateSet
    {
        private readonly Dictionary<EventKey, Delegate> _dicEvents;
        private readonly EventKey _key1 = new EventKey();
        private readonly EventKey _key2 = new EventKey();
        private delegate void Del1(int i);
        private delegate void Del2(int i, string s);
        private readonly Del1 _del1 = new Del1(i => { Console.WriteLine($"Del1, i:{i}"); });
        private readonly Del2 _del2 = new Del2((i, s) => { Console.WriteLine($"Del2, i:{i},s:{s}"); });
        public DynamicDelegateSet()
        {
            this._dicEvents = new Dictionary<EventKey, Delegate>
            {
                { _key1, _del1 },
                { _key2, _del2 },
            };
        }

        public void Invoke()
        {
            var del1 = _dicEvents[_key1];
            del1.DynamicInvoke(new object[] { 5 });

            var del2 = _dicEvents[_key2];
            del2.DynamicInvoke(new object[] { 5, "hehe" });
            del2.DynamicInvoke(5, "hehe");
            //参数不匹配时抛出异常
            del2.DynamicInvoke(new object[] { 5, "hehe", 5 });
        }
    }

    public sealed class EventKey
    { }
}
