using CSharp.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Text;
using System.Xml;

namespace CSharp.xml
{
    public class ResolveXml
    {
        internal void Run()
        {
            CreateCMIOT2001EX();
        }

        private void CreateCMIOT2001EX()
        { 
            var xml = @"<?xml version='1.0' encoding='UTF-8' ?>
<function name='CSharp.Entity.CMIOT_API2001EX' version='1.0.0'>
<body>
    <msisdns>"
+
SecurityElement.Escape(@"1064810210000|1064810210001|1064810210002|1064810210003|1064810210004")
+
@"</msisdns>
<Time>2019-02-28</Time>
</body>
</function>";
            var obj = CreateEntityByXml(xml);
            Console.WriteLine(((CMIOT_API2001EX)obj).msisdns);
            Console.WriteLine(((CMIOT_API2001EX)obj).Time);

            xml = @"<?xml version='1.0' encoding='UTF-8' ?>
<function name='CSharp.Entity.CMIOT_RSP2001EX' version='1.0.0'>
<body>
<result>
<CMIOT_RST2001EX>
<msisdn>1064810210000</msisdn>
<GPRSSTATUS>00</GPRSSTATUS>
</CMIOT_RST2001EX>
<CMIOT_RST2001EX>
<msisdn>1064810210001</msisdn>
<GPRSSTATUS>01</GPRSSTATUS>
</CMIOT_RST2001EX>
</result>
</body>
</function>";
            var rsp = CreateEntityByXml(xml) as CMIOT_RSP2001EX;

            //Error (Entity in Entity)
            foreach (var item in rsp.result)
            {
                Console.WriteLine(item.msisdn);
                Console.WriteLine(item.GPRSSTATUS);
            }

            xml = @"";
        }
        public object CreateEntityByXml(string paramXml)
        {
            try
            {
                // XML 转实体
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(paramXml);
                //string sEntityName = xDoc.SelectSingleNode("DownEntityName").Value;
                XmlNode function = xDoc.SelectSingleNode("function");
                string sEntityName = function.Attributes["name"].Value;
                XmlNodeList bodyNodeList = function.SelectSingleNode("body").ChildNodes;
                // 反射下行实体
                var tEntity = Type.GetType(sEntityName);
                var downDataEntity = Activator.CreateInstance(tEntity);
                //var downDataEntity = ClientCmdRequest.CreateEntity(sEntityName);
                //Type tEntity = downDataEntity.GetType();
                //设置实体属性
                foreach (System.Xml.XmlNode paramNode in bodyNodeList)
                {
                    System.Reflection.PropertyInfo property = tEntity.GetProperty(paramNode.Name);
                    if (property != null)
                    {
                        string sInnerXml = paramNode.InnerXml.Replace("&amp;", "&").Replace("&apos;", "'").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"");
                        // 找到对应属性，给属性赋值
                        if (property.PropertyType.IsEnum)
                        {
                            // 枚举-通过枚举实例赋值
                            EnumConverter enumConverter = new EnumConverter(property.PropertyType);
                            object objEnumValue = enumConverter.ConvertFrom(sInnerXml);
                            property.SetValue(downDataEntity, objEnumValue, null);
                            //property.SetValue(downDataEntity, Convert.ToInt32(sInnerXml), null);
                        }
                        else if (property.PropertyType.IsArray)
                        {

                        }
                        else
                        {

                            object objValue = Convert.ChangeType(sInnerXml, property.PropertyType);
                            property.SetValue(downDataEntity, objValue, null);
                        }
                        continue;
                    }

                    System.Reflection.FieldInfo field = tEntity.GetField(paramNode.Name);
                    if (field != null)
                    {
                        string sInnerXml = paramNode.InnerXml.Replace("&amp;", "&").Replace("&apos;", "'").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"");

                        // 找到对应属性，给属性赋值
                        if (property.PropertyType.IsEnum)
                        {
                            // 枚举-通过枚举实例赋值
                            EnumConverter enumConverter = new EnumConverter(field.FieldType);
                            object objEnumValue = enumConverter.ConvertFrom(sInnerXml);
                            field.SetValue(downDataEntity, objEnumValue);
                            //field.SetValue(downDataEntity, Convert.ToInt32(sInnerXml));
                        }
                        else
                        {
                            object objValue = Convert.ChangeType(sInnerXml, field.FieldType);
                            field.SetValue(downDataEntity, paramNode.Value);
                        }
                        continue;
                    }
                    // 未找到对应属性
                    Console.WriteLine(string.Format("未找到节点{0}对应的属性或字段", paramNode.Name));
                }
                return downDataEntity;
            }
            catch (Exception ex)
            {
                // 未找到对应属性
                Console.WriteLine("创建指令对象时出错" + ex);
            }
            return null;
        }

    }
}
