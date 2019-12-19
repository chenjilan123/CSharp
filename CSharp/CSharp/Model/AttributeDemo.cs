using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Model
{
    [AttributeUsage(
        AttributeTargets.Class
        | AttributeTargets.GenericParameter
        | AttributeTargets.Field
        | AttributeTargets.Property
        | AttributeTargets.Event
        | AttributeTargets.Delegate
        | AttributeTargets.Constructor
        | AttributeTargets.Method
        | AttributeTargets.Parameter
        | AttributeTargets.Struct
        | AttributeTargets.Enum
        | AttributeTargets.Interface
        | AttributeTargets.ReturnValue
        )]
    public class InvisibleAttribute: Attribute
    {
    }

    [Invisible]
    public class InvisibleAttributeTarget<[Invisible]T>
    {
        [Invisible]
        public int field;
        [Invisible]
        public int Property { get; set; }
        [Invisible]
        public event Func<int> Event; //字段是事件？事件
        [Invisible]
        public delegate void Delegate();
        [Invisible]
        public InvisibleAttributeTarget() { }
        [Invisible]
        [return: Invisible] //定义返回值特性
        public int Method([Invisible]int parameter) { return -1; }
    }
    [Invisible]
    public struct IvisibleAttributeTarget { }
    [Invisible] 
    public enum NvisibleAttributeTarget { }
    [Invisible] 
    public interface VisibleAttributeTarget { }
}
