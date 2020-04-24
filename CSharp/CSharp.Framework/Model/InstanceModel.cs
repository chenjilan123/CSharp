using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Framework.Model
{
    public class InstanceModel
    {
        public static InstanceModel Instance { get; private set; } = new InstanceModel();

        public int MyProperty { get;  }


        public InstanceModel()
        {
            Console.WriteLine($"调用了{nameof(InstanceModel)}构造函数");
        }
    }
}
