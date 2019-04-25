using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CSharp.SimpleConfig
{
    class XmlFileHandler
    {
        private static XmlDocument _xdocParamXml = null;
        /// <summary>
        /// xml配置文件路径
        /// </summary>
        private static string _sIniFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "param.xml");

        #region 读取配置
        public static void LoadConfig()
        {
            _xdocParamXml = new XmlDocument();
            _xdocParamXml.Load(_sIniFile);
            SetSysParamValue(SysConfigType.LocalConfigXml);
        }
        #endregion

        #region 动态赋值系统配置
        private static void SetSysParamValue(SysConfigType SysConfigueType)
        {
            try
            {
                Type tType = typeof(Variable);
                System.Reflection.MemberInfo[] members = tType.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static);
                bool bExit = false;
                VariableDataType VariableDataType = VariableDataType.TNoType;
                SymbolType SymbolType = SymbolType.NoSymbol;
                //遍历类中所有公共的成员
                foreach (MemberInfo member in members)
                {
                    bExit = false;
                    object[] AttritubeList = member.GetCustomAttributes(true);
                    if (AttritubeList.Length <= 0)
                        continue;
                    //遍历成员的特性
                    foreach (var attritube in AttritubeList)
                    {
                        CustomAttribute customAttribute = attritube as CustomAttribute;
                        if (customAttribute == null)
                            continue;
                        //特性满足配置类型
                        if (customAttribute.ConfigueType == SysConfigueType)
                        {
                            bExit = true;
                            VariableDataType = customAttribute.DataType;
                            SymbolType = customAttribute.SymbolType;
                        }
                    }
                    //设置参数
                    if (bExit)
                    {
                        SysParamEvaluate(member, VariableDataType, SymbolType);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void SysParamEvaluate(MemberInfo member, VariableDataType variableDataType, SymbolType symbolType)
        {
            try
            {
                Type tType = typeof(Variable);
                object obValue = null;
                object MemberType = null;
                string sMemberName = "";
                string sMemberValue = "";
                //字段
                FieldInfo fieldInfo = tType.GetField(member.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                if (fieldInfo != null)
                {
                    sMemberName = fieldInfo.Name;
                    if (fieldInfo.GetValue(tType) != null)
                    {
                        sMemberValue = fieldInfo.GetValue(tType).ToString();
                        MemberType = fieldInfo.GetValue(tType);
                    }
                }
                //属性
                PropertyInfo propertyInfo = tType.GetProperty(member.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                if (propertyInfo != null)
                {
                    sMemberName = propertyInfo.Name;
                    if (propertyInfo.GetValue(tType) != null)
                    {
                        sMemberValue = propertyInfo.GetValue(tType).ToString();
                        MemberType = propertyInfo.GetValue(tType);
                    }
                }
                if (fieldInfo == null && propertyInfo == null)
                    return;
                switch (variableDataType)
                {
                    case VariableDataType.TString:
                        obValue = GetConfigData(sMemberName, sMemberValue, false);
                        break;
                    case VariableDataType.TInt:
                        int iValue = 0;
                        if (int.TryParse(GetConfigData(sMemberName, sMemberValue, false), out iValue))
                            obValue = iValue;
                        break;
                    case VariableDataType.TStringArray:
                        string sListArray = GetConfigData(sMemberName, string.Empty);
                        if (!string.IsNullOrEmpty(sListArray) && symbolType != SymbolType.NoSymbol)
                        {
                            char chArray = (char)symbolType;
                            obValue = sListArray.Split(chArray);
                        }
                        break;
                    case VariableDataType.TStringList:
                        string sListValue = GetConfigData(sMemberName, string.Empty);
                        if (!string.IsNullOrEmpty(sListValue) && symbolType != SymbolType.NoSymbol)
                        {
                            char chList = (char)symbolType;
                            obValue = new List<string>(sListValue.Split(chList));
                        }
                        break;
                    case VariableDataType.TBool:
                        obValue = GetConfigData(sMemberName, (bool)MemberType ? "1" : "0", false).Equals("1");
                        break;
                    case VariableDataType.TLong:
                        long lValue = 0;
                        if (long.TryParse(GetConfigData(sMemberName, sMemberValue, false), out lValue))
                            obValue = lValue;
                        break;
                    case VariableDataType.TDouble:
                        double dValue = 0;
                        if (double.TryParse(GetConfigData(sMemberName, sMemberValue, false), out dValue))
                            obValue = dValue;
                        break;
                    case VariableDataType.TUint16Change:
                        uint uValue = 0u;
                        if (uint.TryParse(GetConfigData(sMemberName, sMemberValue, false), NumberStyles.HexNumber, null, out uValue))
                            obValue = uValue;
                        break;
                    default:
                        break;
                }
                if (fieldInfo != null)
                    fieldInfo.SetValue(member, obValue, BindingFlags.Public, null, null);
                else if (propertyInfo != null)
                    propertyInfo.SetValue(member, obValue, BindingFlags.Public, null, null, null);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }
        public static string GetConfigData(string sKeyName, string sDefaultValue, bool bIsLocal = true)
        {
            if (bIsLocal && !File.Exists(_sIniFile))
            {
                return sDefaultValue;
            }

            //XmlDocument xdocXmlDoc = GetXmlDocument(sIniFile);
            if (_xdocParamXml == null && bIsLocal)
            {
                _xdocParamXml = GetXmlDocument(_sIniFile);
            }
            if (_xdocParamXml == null)
                return sDefaultValue;

            //string sXPath = string.Format(".//key[@name='{0}']", sKeyName);
            string sXPath = string.Format(".//{0}", sKeyName);
            XmlNode xnodeXmlNode = SelectXmlNode(_xdocParamXml, sXPath);
            if (xnodeXmlNode == null)
            {
                return sDefaultValue;
            }
            return xnodeXmlNode.InnerText;
            //XmlNode xnodeValueAttr = xnodeXmlNode.Attributes.GetNamedItem("value");
            //if (xnodeValueAttr == null)
            //    return sDefaultValue;
            //return xnodeValueAttr.Value;
        }
        #endregion

        #region 从指定XML文件路径获取对应的XML文档对象
        /// <summary>
        /// 从指定XML文件路径获取对应的XML文档对象
        /// </summary>
        /// <param name="sXmlFile">XML文件路径</param>
        /// <returns>XML文档对象</returns>
        private static XmlDocument GetXmlDocument(string sXmlFile)
        {
            if (string.IsNullOrEmpty(sXmlFile))
                return null;
            if (!File.Exists(sXmlFile))
                return null;
            XmlDocument xdocXmlDoc = new XmlDocument();
            try
            {
                xdocXmlDoc.Load(sXmlFile);
            }
            catch
            {
                return null;
            }
            return xdocXmlDoc;
        }
        #endregion

        #region 获取XPath指向的单一XML节点
        /// <summary>
        /// 获取XPath指向的单一XML节点
        /// </summary>
        /// <param name="xnodeRootNode">XPath所在的根节点</param>
        /// <param name="sXPath">XPath表达式</param>
        /// <returns>XmlNode</returns>
        private static XmlNode SelectXmlNode(XmlNode xnodeRootNode, string sXPath)
        {
            if (xnodeRootNode == null || string.IsNullOrEmpty(sXPath))
                return null;
            try
            {
                return xnodeRootNode.SelectSingleNode(sXPath);
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
