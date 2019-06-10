using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Algorithm.Struct
{
    public class Queue
    {
        private int[] _data;
        private int _start;

        public Queue()
        {
            _data = new int[5];
        }

        public bool Enqueue(int value)
        {
            if (_start >= _data.Length)
                return false;
            _data[_start] = value;
            _start++;
            return true;
        }
        public bool Dequeue()
        {
            if (_start >= _data.Length)
                return false;
            _start++;
            return true;
        }

        public int Front()
        {
            return _data[_start];
        }
    }
}
