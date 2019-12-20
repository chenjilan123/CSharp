using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Model
{
    partial class PartialClass
    {
        partial void Info(int i);
        partial void Print(int i);

        public void Run()
        {
            //对分部方法有选择的调用
            this.Info(_iInfo++);
            this.Print(_iPrint++); // _iPrint不会加1，这是编译器的一种优化
            this.Info(_iInfo++);
            this.Print(_iPrint++); // _iPrint不会加1

            //Severity Code    Description Project File Line    Suppression State
            //Error CS0762  Cannot create delegate from method 'PartialClass.Print(int)' because it is a partial method without an implementing declaration CSharp  H:\C#\CSharp\CSharp\CSharp\Model\PartialClass.cs	21	Active
            //Action<int> del = Print;
        }
        public void Print()
        {
            Console.WriteLine($"Info: {_iInfo}, Print: {_iPrint}");
        }

        private int _iInfo = 0;
        private int _iPrint = 0;
    }
    partial class PartialClass
    {
        /// <summary>
        /// 分部方法总是私有的
        /// </summary>
        partial void Info(int i) { }
        private void Information() { }
    }

    class SubPartialClass: PartialClass
    {
        public void Info()
        {
            //base.Info();
            //base.Information();
        }
    }


    partial class C1
    {
        partial void M1(int i);
        partial void M2(int i);

        public void Run()
        {
            int i = 0;
            int j = 0;
            M1(i++);
            M2(j++);

            Console.WriteLine($"i: {i}, j: {j}");
        }
    }

    partial class C1
    {
        partial void M1(int i) { }
    }

}
