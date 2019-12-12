using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

//指定友元程序集
[assembly: InternalsVisibleTo("CSharp.Framework, PublicKey=19a16d15-e49e-478f-aa0c-a888a96ff0d4")]

namespace CSharp.Model
{
    public class Visible 
    {
        private void Method1() { }
        private protected void Method2() { }
        protected private void Method3() { }
        protected void Method4() { }
        internal void Method5() { }
        protected internal void Method6() { }
        internal protected void Method7() { }
        public void Method8() { }
    }
    internal class Visible1
    {
        private void Method1() { }
        private protected void Method2() { }
        protected private void Method3() { }
        protected void Method4() { }
        internal void Method5() { }
        protected internal void Method6() { }
        internal protected void Method7() { }
        public void Method8() 
        { 

        }
    }
    class Visible2 { }
    public interface IVisible { }
    internal interface IVisible1 { }
    interface IVisible2 { }
    // Elements defined in a namespace cannot be explicitly declared as private, protected, protected internal, or private protected CSharp H:\C#\CSharp\CSharp\CSharp\Model\Visible.cs	9	Active
    //private class Visible2 { }

    public static class Void { }
    public static class Void<T> { }
    public struct Viud<T> { }

    public class Viud1 
    {
        public virtual void M1() { }
        public void M2() { }
    }

    public class Viud2 : Viud1
    {
        public sealed override void M1() { }
        //public sealed void M3() { }
    }
}
