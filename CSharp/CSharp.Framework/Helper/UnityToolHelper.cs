using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace CSharp.Framework.Helper
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public  class UnityToolHelper
    {
        #region  转换
        /// <summary>
        /// 将表行 字段 转为相应格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="ColumnName"></param>
        /// <param name="defalult"></param>
        /// <returns></returns>
        public static  T CovertDBValue<T>(DataRow dr, string ColumnName, object defalult) where T : IConvertible
        {
            if (!dr.Table.Columns.Contains(ColumnName))
                return (T)defalult;
            if (dr.IsNull(ColumnName))
                return (T)Convert.ChangeType(defalult, typeof(T));
            if (dr[ColumnName] is DBNull)
                return (T)defalult;
            return (T)Convert.ChangeType(dr[ColumnName], typeof(T));
        }
        #endregion
        /// <summary>
        /// 置换状态
        /// </summary>
        /// <param name="src"></param>
        /// <param name="compare"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static long BitCompare(long src, long compare, long desc)
        {
            if ((src & compare) == 0L)
            {
                return 0L;
            }
            return desc;
        }
        public static T IsNullOrEmpty<T>(object value, object defalult) where T : IConvertible
        {
            if (string.IsNullOrEmpty(value.ToString()))
                return (T)defalult;
            return (T)Convert.ChangeType(value, typeof(T));
        }
        /// <summary>
        /// 转换颜色
        /// </summary>
        /// <param name="iCode"></param>
        /// <returns></returns>
        public static string CovertCodeToColor(int iCode)
        {
            string sColor = "";
            switch (iCode)
            {
                case 1:
                    sColor = "蓝";
                    break;
                case 2:
                    sColor = "黄";
                    break;
                case 3:
                    sColor = "黑";
                    break;
                case 4:
                    sColor = "白";
                    break;
                default:
                    sColor = "其他";
                    break;
            }
            return sColor;
        }
        public static  object CheckIsNull(object obj)    //此方法判断要传递的参数是否为null， 如果为Null, 则返回值DBNLL.Value,主要用户网数据库添加或者更新数据
        {
            if (obj == null)
            {
                return DBNull.Value;
            }
            else
            {
                return obj;
            }
        }

        public static  object CheckIsDbNull(object obj)    //此方法是从数据库中读取数据，如果数据库中的数据为DBNull.Value, 则返回null
        {
            if (Convert.IsDBNull(obj))
            {
                return null;
            }
            else
            {
                return obj;
            }
        }
        
        #region 计算距离
        private const double EARTH_RADIUS = 6378137;
        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return Math.Round(result, 1);
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }
        #endregion

        #region  全局的OrderId
        /// <summary>
        /// 当前发送流水号
        /// </summary>
        private static  int _currMsgSN = -1; 
        /// <summary>
        /// 获取一个流水号
        /// </summary>
        /// <returns></returns>
        public static  int GetMsgSN()
        {
            Interlocked.CompareExchange(ref _currMsgSN, -1, UInt16.MaxValue);
            return Interlocked.Increment(ref _currMsgSN);
        }

        #endregion

        #region 经纬度转换
        /// <summary>
        ///  转换经纬度
        /// </summary>
        /// <param name="sCoordinate"></param>
        /// <param name="iFenScale">分的比例 1000/1  ..</param>
        /// <param name="FenLenght">分占的长度</param>
        /// <returns></returns>
        public static double ConvertCoordinate(string sCoordinate, int iFenScale = 1000, int FenLenght = 5)
        {
            sCoordinate = sCoordinate.TrimStart('0');
            if (sCoordinate.Length < FenLenght)//位数不足
                return 0;
            string sDu = sCoordinate.Substring(0, sCoordinate.Length - FenLenght).TrimStart('0');
            var iDu = Convert.ToInt32(sDu);
            string sFen = sCoordinate.Substring(sCoordinate.Length - FenLenght, FenLenght).TrimStart('0');
            var dFen = Convert.ToInt32(sFen.PadLeft(FenLenght, '0')) / (iFenScale * 1.0) * ((1 / 60.0));
            return Convert.ToDouble((iDu + dFen).ToString("f6"));
        }
        public static double LongToLng(ulong lLng, int iPrecision = 6)
        {
            double Precision = 0;
            if (iPrecision == 6)
                Precision = 1000000;
            else
                Precision = Math.Pow(10, iPrecision);
            return lLng / Precision;
        }
        #endregion

        #region Json 操作
        /// <summary>
        /// 从json字符串 获取数据 数组(json串比较短的时候)
        /// </summary>
        /// <param name="json"></param>
        /// <param name="cloumnName">列名</param>
        /// <param name="isMulti">是否多个</param>
        /// <returns></returns>
        public static List<string> GetJsonArray(string json, string cloumnName, bool isMulti = false)
        {
            var index = json.IndexOf(cloumnName);
            List<string> response = new List<string>();
            while (index > 0)
            {
                json = json.Substring(index, json.Length - index - 1);
                var flag = json.IndexOf(",");
                var tempValue = string.Empty;
                if (flag >= 0)
                    tempValue = json.Substring(0, flag);
                else
                    tempValue = json;
                tempValue = tempValue.Replace('"', ' ').Trim();
                if (tempValue != "" && tempValue.IndexOf(":") > 0)
                {
                    response.Add(tempValue.Split(':')[1].ToString());
                }
                if (!isMulti)
                    break;
                json = json.Substring(flag, json.Length - flag - 1);
                index = json.IndexOf(cloumnName);
            }
            return response;
        }
        #endregion

        #region 数据压缩
        /// <summary>
        /// GZip压缩函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static  byte[] GZipCompress(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Compress))
                {
                    gZipStream.Write(data, 0, data.Length);
                    gZipStream.Close();
                }
                return stream.ToArray();
            }
        }
        /// <summary>
        /// GZip解压函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GZipDecompress(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream gZipStream = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
                {
                    byte[] bytes = new byte[data.Length*2];
                    int n;
                    while ((n = gZipStream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        stream.Write(bytes, 0, n);
                    }
                    gZipStream.Close();
                }

                return stream.ToArray();
            }
        }
        /// <summary>
        /// Deflate解压函数
        /// JS:var details = eval('(' + utf8to16(zip_depress(base64decode(hidEnCode.value))) + ')')对应的C#压缩方法
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static  byte[] DeflateDecompress(byte[] buffer)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                ms.Write(buffer, 0, buffer.Length);
                ms.Position = 0;
                using (System.IO.Compression.DeflateStream stream = new System.IO.Compression.DeflateStream(ms, System.IO.Compression.CompressionMode.Decompress))
                {
                    stream.Flush();
                    long nSize =stream.Length;    //假设字符串不会超过16K
                    byte[] decompressBuffer = new byte[nSize];
                    int nSizeIncept = stream.Read(decompressBuffer, 0, decompressBuffer.Length);
                    stream.Close();
                    return decompressBuffer;   //转换为普通的字符串
                }
            }
        }
        /// <summary>
        /// Deflate压缩函数
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public string DeflateCompress(string strSource)
        {
            if (strSource == null || strSource.Length > 8 * 1024)
                throw new System.ArgumentException("字符串为空或长度太大！");
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(strSource);
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (System.IO.Compression.DeflateStream stream = new System.IO.Compression.DeflateStream(ms, System.IO.Compression.CompressionMode.Compress, true))
                {
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Close();
                }
                byte[] compressedData = ms.ToArray();
                ms.Close();
                return Convert.ToBase64String(compressedData);      //将压缩后的byte[]转换为Base64String
            }
        }
        #endregion

        #region  选择解压方式
        /// <summary>
        /// 数据解压 根据解压类型
        /// </summary>
        /// <param name="sCompressType"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static  byte[] DeCompressByType(string sCompressType,byte[] source)
        {
            byte[] rsp = new byte[0];
            switch (sCompressType)
            {
                case "gzip":
                    rsp = GZipDecompress(source);
                    break;
                case "deflate":
                    rsp = DeflateDecompress(source);
                    break;
                default:
                    rsp = source;//未找编码类型的返回源数据
                    break;
            }
            return rsp;
        }
        #endregion
    }
    /// <summary>
    /// 坐标系转换
    /// </summary>
    public class MapTransfer
    {
        /*
        WGS-84：是国际标准，GPS坐标（Google Earth使用、或者GPS模块）
        GCJ-02：中国坐标偏移标准，Google Map、高德、腾讯使用
        BD-09：百度坐标偏移标准，Baidu Map使用

       代码参考：http://bbs.lbsyun.baidu.com/forum.php?mod=viewthread&tid=10923
        */

        static double pi = 3.14159265358979324;
        static double a = 6378245.0;
        static double ee = 0.00669342162296594323;
        public const double x_pi = 3.14159265358979324 * 3000.0 / 180.0;

        /// <summary>
        /// gps坐标转换成百度坐标，小数点前4位为准确坐标
        /// </summary>
        /// <param name="lat">纬度</param>
        /// <param name="lon">经度</param>
        /// <returns></returns>
        public static double[] wgs2bd(double lat, double lon)
        {
            double[] dwgs2gcj = wgs2gcj(lat, lon);
            double[] dgcj2bd = gcj2bd(dwgs2gcj[0], dwgs2gcj[1]);
            return dgcj2bd;
        }

        public static double[] gcj2bd(double lat, double lon)
        {
            double x = lon, y = lat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
            double bd_lon = z * Math.Cos(theta) + 0.0065;
            double bd_lat = z * Math.Sin(theta) + 0.006;
            return new double[] { bd_lat, bd_lon };
        }

        public static double[] bd2gcj(double lat, double lon)
        {
            double x = lon - 0.0065, y = lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
            double gg_lon = z * Math.Cos(theta);
            double gg_lat = z * Math.Sin(theta);
            return new double[] { gg_lat, gg_lon };
        }

        public static double[] wgs2gcj(double lat, double lon)
        {
            double dLat = transformLat(lon - 105.0, lat - 35.0);
            double dLon = transformLon(lon - 105.0, lat - 35.0);
            double radLat = lat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            double mgLat = lat + dLat;
            double mgLon = lon + dLon;
            double[] loc = { mgLat, mgLon };
            return loc;
        }

        private static double transformLat(double lat, double lon)
        {
            double ret = -100.0 + 2.0 * lat + 3.0 * lon + 0.2 * lon * lon + 0.1 * lat * lon + 0.2 * Math.Sqrt(Math.Abs(lat));
            ret += (20.0 * Math.Sin(6.0 * lat * pi) + 20.0 * Math.Sin(2.0 * lat * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lon * pi) + 40.0 * Math.Sin(lon / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(lon / 12.0 * pi) + 320 * Math.Sin(lon * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        private static double transformLon(double lat, double lon)
        {
            double ret = 300.0 + lat + 2.0 * lon + 0.1 * lat * lat + 0.1 * lat * lon + 0.1 * Math.Sqrt(Math.Abs(lat));
            ret += (20.0 * Math.Sin(6.0 * lat * pi) + 20.0 * Math.Sin(2.0 * lat * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lat * pi) + 40.0 * Math.Sin(lat / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(lat / 12.0 * pi) + 300.0 * Math.Sin(lat / 30.0 * pi)) * 2.0 / 3.0;
            return ret;
        }
    }
}
