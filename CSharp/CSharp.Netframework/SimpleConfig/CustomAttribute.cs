using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.SimpleConfig
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class CustomAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ConfigueType">读取的配置类型</param>
        /// <param name="DataType">数据类型</param>
        /// <param name="SymbolType">分隔符号</param>
        public CustomAttribute(SysConfigType ConfigueType, VariableDataType DataType, SymbolType SymbolType = SymbolType.NoSymbol)
        {
            SysConfigType = ConfigueType;
            dataType = DataType;
            symbolType = SymbolType;
        }
        /// <summary>
        /// 配置类型
        /// </summary>
        private SysConfigType SysConfigType;
        public SysConfigType ConfigueType
        {
            get
            {
                return SysConfigType;
            }
        }
        /// <summary>
        /// 数据类型
        /// </summary>
        private VariableDataType dataType;
        public VariableDataType DataType
        {
            get
            {
                return dataType;
            }
        }
        /// <summary>
        /// 分隔符号
        /// </summary>
        private SymbolType symbolType;
        public SymbolType SymbolType
        {
            get
            {
                return symbolType;
            }
        }
    }
}
