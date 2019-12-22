using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace CSharp.Model
{
    sealed class ParameterProperty
    {
        //自定义属性名称
        //[IndexerName("Parameter")]  //编译器特性
        //protected bool this[int i, int j]
        bool this[int i, int j]
        {
            get
            {
                return true;
            }
        }

        //[IndexerName("Parameter")]
        //public bool this[int i]
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        //protected bool this[int i]
        //{
        //    public get { return true; }
        //    set { }
        //}


        //索引器的属性名为Item, 不能再定义Item属性
        //public bool Item { get; set; }

        //与索引器方法签名重复了
        //public bool get_Item(int i, int j)
        //{
        //    return true;
        //}

        public bool GetItem(int i, int j)
        {
            //访问语法
            return this[i, j];
        }
    }
}
