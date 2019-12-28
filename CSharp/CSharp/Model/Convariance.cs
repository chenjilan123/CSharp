using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Model
{
    public interface Convariance<out T>
    {
    }

    public interface IContravariance<in TIn, out TOut>
    {
        TOut GetValue(TIn param);
    }


    public class Contravariance : IContravariance<Object2, Object2>
    {
        public Object2 GetValue(Object2 param)
        {
            param.GetMessage();
            return param;
        }
    }

    public class Contravariance1 : IContravariance<int, int>
    {
        public int GetValue(int param)
        {
            return 1;
        }
    }

    public class Object1
    {
        public virtual void GetMessage()
        {
            Console.WriteLine("Message from object1");
        }
    }

    public class Object2 : Object1
    {
        public override void GetMessage()
        {
            Console.WriteLine("Message from object2");
        }
    }
    public class Object3 : Object2
    {
        public override void GetMessage()
        {
            Console.WriteLine("Message from object3");
        }
    }


    public class Class4<T> where T: IComparable<T>, IEnumerable<T>
    {
        public void Compare(T t1, T t2)
        {
            t1.CompareTo(t2);
        }
    }


    //泛型约束与重载
    public interface IClass4<T> where T:IComparable<T>
    {
        void Method<T1, T2>() where T2: T1,new() where T1:T;
        void Method<T1>();
    }

    public interface IClass4<T1, T2> 
        where T1: Delegate 
        where T2: MulticastDelegate
    {

    }
}
