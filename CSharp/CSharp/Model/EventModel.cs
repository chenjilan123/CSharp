using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSharp.Model
{
    public class EventModel
    {
        public event EventHandler<Event0EventArgs> Event0;
        //public event EventHandler<EventArgs> Event1;

        protected virtual void OnEvent0(Event0EventArgs e)
        {
            //传统方式
            //var temp = Volatile.Read(ref Event0);
            //temp?.Invoke(this, e);

            //封装到通用扩展方法。
            e.Raise(this, ref Event0);

            //var e1 = EventArgs.Empty;
            //e1.Raise(this, ref Event1);
        }

        internal void Run()
        {
            //Do something

            //Triger event
            OnEvent0(new Event0EventArgs(1));
        }
    }

    public class Event0EventArgs : EventArgs
    {
        private readonly int _value;

        //只读，可防止事件处理方法改变他。
        public int Value { get { return this._value; } }
        public Event0EventArgs(int value)
        {
            this._value = value;
        }
    }
}
