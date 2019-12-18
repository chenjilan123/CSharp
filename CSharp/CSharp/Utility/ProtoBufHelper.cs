using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using System.IO;

namespace CSharp.Utility
{
    /// <summary>
    /// ProtoBuf 实例类
    /// </summary>
    public  class ProtoBufHelper
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static  T Deserialize<T>(byte[] data)
        {
            T rsp=default(T);
            try
            {
                using(MemoryStream ms = new MemoryStream(data)){
                    rsp =(T)Serializer.Deserialize<T>(ms);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("ProtoBuf Deserialize Error." + ex);
            }
            return rsp;
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static  byte[] Serialize<T>(T entity)
        {
            byte[] data =new  byte[0];
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Serializer.Serialize<T>(ms,entity);
                    data = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProtoBuf Serialize Error." + ex);
            }
            return data;
        }
    }
}
