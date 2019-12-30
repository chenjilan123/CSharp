using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Model
{
    public class ActivatorModel
    {
        private const string name = nameof(ActivatorModel);

        public string Name { get { return nameof(ActivatorModel); } }

        private string GetName()
        {
            return nameof(ActivatorModel);
        }

        public ActivatorModel()
        {
            Console.WriteLine($"{name},无参构造函数");
        }

        public ActivatorModel(int i)
        {
            Console.WriteLine($"{Name},int参数构造函数");
        }
        //public ActivatorModel(ushort i)
        //{
        //    Console.WriteLine($"{Name},ushort参数构造函数");
        //}
        public ActivatorModel(string i)
        {
            Console.WriteLine($"{GetName() + ""},string参数构造函数");
        }
    }
}
