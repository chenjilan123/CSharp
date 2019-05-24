using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace CSharp.Framework.Helper
{
    public static  class SerializeHelper
    {
        #region 数据的序列化
        /// <summary>
        /// 序列化成byte存到文件中去(Binary)
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <param name="o">参数类型</param>
        public static void BinaryToFile(string filePath, string fileName, object o)
        {
            if(!Directory.Exists(filePath))
            {
                try
                {
                    Directory.CreateDirectory(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString()); 
                }
            }

            FileStream fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.OpenOrCreate, FileAccess.Write);
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fileStream, o);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); 
            }
            finally
            {
                fileStream.Flush();
                fileStream.Dispose();
                fileStream.Close();
            }
        }
        /// <summary>
        /// 从文件中反序列化出数据（Binary）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sFilePath"></param>
        /// <param name="o">T</param>
        /// <returns></returns>
        public static T BinaryFromFile<T>(string sFilePath)
        {
            FileStream fileStream = new FileStream(sFilePath, FileMode.Open);
            T responseClass=default(T);
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
               // bf.Binder = new UBinder();//
                responseClass= (T)bf.Deserialize(fileStream);
                //File.Delete(sFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                fileStream.Flush();
                fileStream.Dispose();
                fileStream.Close();
            }
            return responseClass;
        }
        //public class UBinder : SerializationBinder
        //{
        //    public override Type BindToType(string assemblyName, string typeName)
        //    {
        //        System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
        //        return ass.GetType(typeName);
        //    }
        //}
        #endregion
            
        #region  Json序列化
        // 将对象序列化成 json 字符串
        public static string ObjectToJson(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();

            return myJson.Serialize(obj);
        }

        // 将 json 字符串反序列化成对象
        public static object JsonToObject(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();

            return myJson.DeserializeObject(json);
        }
        // 将 json 字符串反序列化成对象
        public static T JsonToObject<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();

            return myJson.Deserialize<T>(json);
        }
        #endregion
    }
}
