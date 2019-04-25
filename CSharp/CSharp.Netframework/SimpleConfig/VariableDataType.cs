using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.SimpleConfig
{
    /// <summary>
    /// 数据类型
    /// </summary>
    public enum VariableDataType
    {
        TNoType = 0, //默认值
        TString = 1, //string 
        TInt = 2,    //int
        TStringArray = 4, //string[]
        TStringList = 8,//List<string>
        TBool = 16,//bool
        TLong = 32,//long
        TDouble = 64,//double
        TUint = 128,//uint
        TUint16Change = 256, //uint 16转10
    }

}
