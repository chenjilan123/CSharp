using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CSharp.Model
{
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct NullValue<T> where T:struct
    {
        private Boolean _hasValue;
        private T value;

        public NullValue(T value) : this()
        {
            this.value = value;
        }

        public bool HasValue { get { return this._hasValue; } }

        public static implicit operator NullValue<T>(T value)
        {
            return new NullValue<T>(value);
        }
        public static explicit operator T(NullValue<T> value)
        {
            return value.value;
        }
    }
}
