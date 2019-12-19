using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CSharp.Helper
{
    public class BinaryHelper
    {
        public static T[] CloneRange<T>(IList<T> source, int offset, int length)
        {
            T[] target = null;
            try
            {
                var array = source as T[];

                if (array != null)
                {
                    target = new T[length];
                    Array.Copy(array, offset, target, 0, length);
                    return target;
                }

                target = new T[length];

                for (int i = 0; i < length; i++)
                {
                    target[i] = source[offset + i];
                }
                return target;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (target != null)
                    target = null;
            }
        }

        #region 转义、反转义
        /// <summary>
        /// 字节反转移（去除头尾的7e，7d01->7d, 7d02->7e）
        /// </summary>
        /// <param name="data">data进行转义</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>true-成功 false-失败</returns>
        public static bool ByteDescape(byte[] data, out byte[] reData, ref string errMsg)
        {
            List<byte> lstData = new List<byte>();
            reData = null;
            int ilength = data.Length;
            for (int i = 1; i < ilength - 1; i++) //去除头尾的7E
            {
                if (data[i] != 0x7D || i == ilength - 2)
                {
                    lstData.Add(data[i]);
                }
                else if ((data[i + 1] == 0x02))
                {
                    lstData.Add(0x7E);
                    i++;
                }
                else if ((data[i + 1] == 0x01))
                {
                    lstData.Add(0x7D);
                    i++;
                }
                else
                {
                    errMsg = "终端数据包含非法转义字符7D " + data[i + 1].ToString("X2");
                    return false;
                }
            }
            reData = lstData.ToArray();
            return true;
        }

        public static byte[] ByteEscape(List<byte> data)
        {
            List<byte> reData = new List<byte>();
            int ilength = data.Count;
            reData.Add(0x7E);
            for (int i = 0; i < ilength; i++)
            {
                if (data[i] == 0x7D)
                {
                    reData.Add(0x7D);
                    reData.Add(0x01);
                }
                else if (data[i] == 0x7E)
                {
                    reData.Add(0x7D);
                    reData.Add(0x02);
                }
                else
                {
                    reData.Add(data[i]);
                }
            }
            reData.Add(0x7E);
            return reData.ToArray();
        }
        #endregion

        /// <summary>
        /// 字节反转义（去除头尾的5b 5d，5a01->5a, 5a02->5a,5e01->5e, 5e02->5e）
        /// </summary>
        /// <param name="data">data进行转义</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>true-成功 false-失败</returns>
        public static bool Byte809Descape(byte[] data, out byte[] reData, ref string errMsg)
        {
            List<byte> lstData = new List<byte>();
            reData = null;
            int ilength = data.Length;
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] == 0x5a)
                {
                    if (data[i + 1] == 0x01)
                        lstData.Add(0x5b);
                    else if (data[i + 1] == 0x02)
                        lstData.Add(0x5a);
                    i++;//下一个字符不处理 不添加到数据数组里面
                }
                else if (data[i] == 0x5e)
                {
                    if (data[i + 1] == 0x01)
                        lstData.Add(0x5d);
                    else if (data[i + 1] == 0x02)
                        lstData.Add(0x5e);
                    i++;//下一个字符不处理 不添加到数据数组里面
                }
                else
                {
                    if (i != ilength - 1)//data[i] != 0x7D ||
                    {
                        lstData.Add(data[i]);
                    }
                }
                //else
                //{
                //    errMsg = "终端数据包含非法转义字符7D " + data[i + 1].ToString("X2");
                //    return false;
                //}
            }
            reData = lstData.ToArray();
            return true;
        }
        /// <summary>
        /// 字节转义（加上头尾的5b 5d，5a->5a01,5e->5e01）
        /// </summary>
        /// <param name="data">data进行转义</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>true-成功 false-失败</returns>
        public static byte[] Byte809Escape(List<byte> data)
        {
            List<byte> lstData = new List<byte>();
            int ilength = data.Count;
            lstData.Add(0x5b);
            for (int i = 0; i < ilength; i++)
            {
                if (data[i] == 0x5b)
                {
                    lstData.Add(0x5a);
                    lstData.Add(0x01);
                }
                else if (data[i] == 0x5a)
                {
                    lstData.Add(0x5a);
                    lstData.Add(0x02);
                }
                else if (data[i] == 0x5d)
                {
                    lstData.Add(0x5e);
                    lstData.Add(0x01);
                }
                else if (data[i] == 0x5e)
                {
                    lstData.Add(0x5e);
                    lstData.Add(0x02);
                }
                else
                {
                    lstData.Add(data[i]);
                }
            }
            lstData.Add(0x5d);
            return lstData.ToArray();
        }

        #region SimNum转BCD、BCD转SimNum，String转BCD、BCD转String
        /// <summary>
        /// SimNum转BCD
        /// </summary>
        /// <param name="simNum"></param>
        /// <returns></returns>
        public static byte[] SimNumToBCD(string simNum)
        {
            simNum = simNum.PadLeft(12, '0');
            byte[] ret = new byte[6];
            for (int i = 0; i < 6; i++)
                ret[i] = Convert.ToByte(simNum.Substring(i * 2, 2), 16);
            return ret;
        }
        /// <summary>
        /// BCD转SimNum
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BCDToSimNum(byte[] bytes, int startIndex = 0)
        {
            StringBuilder returnStr = new StringBuilder(12);
            int iLength = Math.Min(bytes.Length - startIndex, 6);
            for (int i = startIndex; i < startIndex + iLength; i++)
            {
                returnStr.Append(bytes[i].ToString("X2"));
            }
            return returnStr.ToString().TrimStart('0');
        }

        /// <summary>
        /// String转BCD
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] StringToBCD(string value)
        {
            int iLength = value.Length / 2;
            byte[] ret = new byte[iLength];
            for (int i = 0; i < iLength; i++)
                ret[i] = Convert.ToByte(value.Substring(i * 2, 2), 16);
            return ret;

        }
        /// <summary>
        /// BCD转String
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BCDToString(byte[] bytes, int startIndex, int length)
        {
            StringBuilder returnStr = new StringBuilder();
            for (int i = startIndex; i < startIndex + length; i++)
            {
                returnStr.Append(bytes[i].ToString("X2"));
            }

            return returnStr.ToString();
        }
        public static string BCD5ToTimeStr(byte[] bytes, int startIndex, int length)
        {
            StringBuilder returnStr = new StringBuilder();

            for (int i = startIndex; i < startIndex + length; i++)//BCD[5]  01-02-03-0123
            {
                returnStr.Append(bytes[i].ToString("X2") + "-");
                if ((i - startIndex) == 3)
                {
                    returnStr.Remove(returnStr.Length - 1, 1);//去掉“-”，毫秒部分组合在一起0123
                }
            }

            returnStr.Remove(returnStr.Length - 1, 1);
            return returnStr.ToString();
        }
        /// <summary>
        /// BCD转为dateTime
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="format">日期时间格式 如：yyyyMMddHHmmss, yyyyMMdd 等</param>
        /// <returns></returns>
        //public static DateTime BCDToDateTime(byte[] bytes,int startIndex,int length, string format)
        //{
        //    DateTime dateTime;
        //    try
        //    {
        //        string sDataTime = BinaryHelper.BCDToString(bytes, startIndex, length);

        //        bool bResult = DateTime.TryParseExact(sDataTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
        //        if (!bResult)
        //            dateTime = GlobalLib.FailGpsTime;
        //    }
        //    catch
        //    {
        //        dateTime = GlobalLib.FailGpsTime;
        //    }
        //    return dateTime;

        //DateTime rspResult;
        //if(startIndex +length <= bytes.Length)
        //{
        //    byte Year=0, Month=0, Day=0, Hour=0, Min=0, Sencond=0;
        //    if(length==6)
        //    {
        //        Year = bytes[startIndex];
        //        Month =bytes[startIndex+1];
        //        Day = bytes[startIndex+2];
        //        Hour = bytes[startIndex+3];
        //        Min = bytes[startIndex+4];
        //        Sencond = bytes[startIndex+5];
        //    }
        //    else if(length==4) //BCD 4的时候
        //    {
        //        Year = bytes[startIndex + 1];//年的后两位
        //        Month = bytes[startIndex + 2];
        //        Day = bytes[startIndex + 3];
        //        Hour = 0;
        //        Min = 0;
        //        Sencond = 0;
        //    }

        //    if((Year>0 && Year <100) && (Month >0&& Month<13) && (Day >0 && Day<32) &&(Hour>=0 && Hour <24) &&(Min>=0 && Min <=59) && (Sencond>=0 && Sencond <=59))
        //    {
        //        string sTempTime = "20"+Year.ToString("X2") + "-" + Month.ToString("X2") + "-" + Day.ToString("X2") + " " + Hour.ToString("X2") + ":" + Min.ToString("X2") + ":" + Sencond.ToString("X2");
        //        rspResult = Convert.ToDateTime(sTempTime);
        //    }
        //    else
        //    {
        //        rspResult = GlobalLib.FailGpsTime;
        //    }
        //}
        //else
        //{
        //    rspResult = GlobalLib.FailGpsTime;
        //}
        //return rspResult;
        //}
        /// <summary>
        /// 日期转换为BCD
        /// </summary>
        /// <param name="Time"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] DateTimeToBCD(DateTime Time, int length)
        {
            byte[] rspBytes = new byte[6];
            rspBytes[0] = Convert.ToByte(Time.Year.ToString().Substring(2, 2));
            rspBytes[1] = (byte)Time.Month;
            rspBytes[2] = (byte)Time.Day;
            rspBytes[3] = (byte)Time.Hour;
            rspBytes[4] = (byte)Time.Minute;
            rspBytes[5] = (byte)Time.Second;
            return rspBytes;
        }
        #endregion

        #region 整数转byte数组        
        /// <summary>
        /// 以字节数组的形式返回指定的 32 位有符号整数值
        /// </summary>
        /// <param name="value">要转换的数字</param>
        /// <returns>长度为 4 的字节数组</returns>
        public static byte[] GetBytes(int value)
        {
            byte[] reBytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(reBytes);
            }
            return reBytes;
        }
        /// <summary>
        /// 以字节数组的形式返回指定的 64 位有符号整数值
        /// </summary>
        /// <param name="value">要转换的数字</param>
        /// <returns>长度为 8 的字节数组</returns>
        public static byte[] GetBytes(long value)
        {
            byte[] reBytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(reBytes);
            }
            return reBytes;
        }
        /// <summary>
        /// 以字节数组的形式返回指定的 16 位有符号整数值。
        /// </summary>
        /// <param name="value">要转换的数字</param>
        /// <returns>长度为 2 的字节数组</returns>
        public static byte[] GetBytes(short value)
        {
            byte[] reBytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(reBytes);
            }
            return reBytes;
        }
        /// <summary>
        /// 以字节数组的形式返回指定的 32 位无符号整数值
        /// </summary>
        /// <param name="value">要转换的数字</param>
        /// <returns>长度为 4 的字节数组</returns>
        public static byte[] GetBytes(uint value)
        {
            byte[] reBytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(reBytes);
            }
            return reBytes;
        }
        /// <summary>
        /// 以字节数组的形式返回指定的 64 位无符号整数值
        /// </summary>
        /// <param name="value">要转换的数字</param>
        /// <returns>长度为 8 的字节数组</returns>
        public static byte[] GetBytes(ulong value)
        {
            byte[] reBytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(reBytes);
            }
            return reBytes;
        }
        /// <summary>
        /// 以字节数组的形式返回指定的 16 位无符号整数值
        /// </summary>
        /// <param name="value">要转换的数字</param>
        /// <returns>长度为 2 的字节数组</returns>
        public static byte[] GetBytes(ushort value)
        {
            byte[] reBytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(reBytes);
            }
            return reBytes;
        }
        #endregion

        #region byte数组转整数       
        /// <summary>
        /// 返回由字节数组中指定位置的两个字节转换来的 16 位有符号整数
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">value 内的起始位置</param>
        /// <returns>由两个字节构成、从 startIndex 开始的 16 位有符号整数</returns>
        /// <exception cref="System.ArgumentException">startIndex 等于 value 减 1 的长度</exception>
        /// <exception cref="System.ArgumentNullException">value 为 null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex 小于零或大于 value 减 1 的长度</exception>
        public static short ToInt16(byte[] value, int startIndex)
        {
            byte[] btTmp = CloneRange<byte>(value, startIndex, 2);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(btTmp);
            }
            return BitConverter.ToInt16(btTmp, 0);
        }
        /// <summary>
        /// 返回由字节数组中指定位置的四个字节转换来的 32 位有符号整数
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">value 内的起始位置</param>
        /// <returns>由四个字节构成、从 startIndex 开始的 32 位有符号整数</returns>
        /// <exception cref="System.ArgumentException">startIndex 大于等于 value 减 3 的长度，且小于等于 value 减 1 的长度</exception>
        /// <exception cref="System.ArgumentNullException">value 为 null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex 小于零或大于 value 减 1 的长度</exception>
        public static int ToInt32(byte[] value, int startIndex)
        {
            byte[] btTmp = CloneRange<byte>(value, startIndex, 4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(btTmp);
            }
            return BitConverter.ToInt32(btTmp, 0);
        }
        /// <summary>
        /// 返回由字节数组中指定位置的八个字节转换来的 64 位有符号整数
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">value 内的起始位置</param>
        /// <returns>由八个字节构成、从 startIndex 开始的 64 位有符号整数</returns>
        /// <exception cref="System.ArgumentException">startIndex 大于等于 value 减 7 的长度，且小于等于 value 减 1 的长度</exception>
        /// <exception cref="System.ArgumentNullException">value 为 null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex 小于零或大于 value 减 1 的长度</exception>
        public static long ToInt64(byte[] value, int startIndex)
        {
            byte[] btTmp = CloneRange<byte>(value, startIndex, 8);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(btTmp);
            }
            return BitConverter.ToInt64(btTmp, 0);
        }
        /// <summary>
        /// 返回由字节数组中指定位置的两个字节转换来的 16 位无符号整数
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">value 内的起始位置</param>
        /// <returns>由两个字节构成、从 startIndex 开始的 16 位无符号整数</returns>
        /// <exception cref="System.ArgumentException">startIndex 等于 value 减 1 的长度</exception>
        /// <exception cref="System.ArgumentNullException">value 为 null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex 小于零或大于 value 减 1 的长度</exception>
        public static ushort ToUInt16(byte[] value, int startIndex)
        {
            byte[] btTmp = CloneRange<byte>(value, startIndex, 2);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(btTmp);
            }
            return BitConverter.ToUInt16(btTmp, 0);
        }
        /// <summary>
        /// 返回由字节数组中指定位置的四个字节转换来的 32 位无符号整数
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">value 内的起始位置</param>
        /// <returns>由四个字节构成、从 startIndex 开始的 32 位无符号整数</returns>
        /// <exception cref="System.ArgumentException">startIndex 大于等于 value 减 3 的长度，且小于等于 value 减 1 的长度</exception>
        /// <exception cref="System.ArgumentNullException">value 为 null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex 小于零或大于 value 减 1 的长度</exception>
        public static uint ToUInt32(byte[] value, int startIndex)
        {
            byte[] btTmp = CloneRange<byte>(value, startIndex, 4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(btTmp);
            }
            return BitConverter.ToUInt32(btTmp, 0);
        }
        /// <summary>
        /// 返回由字节数组中指定位置的八个字节转换来的 64 位无符号整数
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">value 内的起始位置</param>
        /// <returns>由八个字节构成、从 startIndex 开始的 64 位无符号整数</returns>
        /// <exception cref="System.ArgumentException">startIndex 大于等于 value 减 7 的长度，且小于等于 value 减 1 的长度</exception>
        /// <exception cref="System.ArgumentNullException">value 为 null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex 小于零或大于 value 减 1 的长度</exception>
        public static ulong ToUInt64(byte[] value, int startIndex)
        {
            byte[] btTmp = CloneRange<byte>(value, startIndex, 8);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(btTmp);
            }
            return BitConverter.ToUInt64(btTmp, 0);
        }
        
        #endregion

        #region byte数组转字符串
        /// <summary>
        /// byte数组转字符串
        /// </summary>
        public static string ToGBKString(byte[] value, int startIndex, int length)
        {
            return Encoding.GetEncoding("GBK").GetString(value, startIndex, length);
        }

        public static string ToASCIIString(byte[] value, int startIndex, int length)
        {
            return Encoding.ASCII.GetString(value, startIndex, length);
        }
        #endregion

        #region 字符串转byte数组
        /// <summary>
        /// 字符串转byte数组（GBK编码）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetGBKBytes(string value)
        {
            if (string.IsNullOrEmpty(value))
                return new byte[0];
            return Encoding.GetEncoding("GBK").GetBytes(value);
        }
        /// <summary>
        /// 字符串转byte数组（GBK编码） 位数不足 补0x00
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetGBKBytes(string value, int length)
        {
            if (value == null)
            {
                value = "";
            }
            List<byte> rspByte = new List<byte>();
            byte[] tempByte = Encoding.GetEncoding("GBK").GetBytes(value);
            if (length >= tempByte.Length)
            {
                rspByte.AddRange(tempByte);
                for (int i = tempByte.Length; i < length; i++)
                {
                    rspByte.Add(0x00);
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    rspByte.Add(tempByte[i]);
                }
            }
            return rspByte.ToArray();
        }
        /// <summary>
        /// 字符串转byte数组（ASCII编码）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetASCIIString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return new byte[0];
            return Encoding.ASCII.GetBytes(value);
        }
        public static string CheckSimNum(string SimNum, int LimitLen)
        {
            int tempLen = SimNum.Length;
            if (tempLen < LimitLen)
            {
                for (int i = 0; i < (LimitLen - tempLen); i++)
                {
                    SimNum = '0' + SimNum;
                }
            }
            return SimNum;
        }
        #endregion

        #region 时间转为byte
        /// <summary>
        /// 将日期转换为 ddMMYYYY 4字节
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static byte[] DateFormat_ddMMyy(DateTime dt)
        {
            List<byte> rspByte = new List<byte>();
            rspByte.Add((byte)dt.Day);
            rspByte.Add((byte)dt.Month);
            rspByte.AddRange(GetBytes((ushort)dt.Year));
            return rspByte.ToArray();
        }
        /// <summary>
        /// 将日期转换为 ddMMYYYY 4字节
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static byte[] DateFormat_yyMMdd(DateTime dt)
        {
            List<byte> rspByte = new List<byte>();
            rspByte.AddRange(GetBytes((ushort)dt.Year));
            rspByte.Add((byte)dt.Month);
            rspByte.Add((byte)dt.Day);
            return rspByte.ToArray();
        }
        /// <summary>
        /// 将日期转换为 hhMMss 3字节
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static byte[] DateFormat_hhMMss(DateTime dt)
        {
            List<byte> rspByte = new List<byte>();
            rspByte.Add((byte)dt.Hour);
            rspByte.Add((byte)dt.Minute);
            rspByte.Add((byte)dt.Second);
            return rspByte.ToArray();
        }

        //public static byte[] DateToUTC(DateTime dt)
        //{
        //    double dResult = 0;
        //    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        //    dResult = (dt - startTime).TotalSeconds;
        //    return GetBytes((long)dResult);
        //    /*旧*/
        //    //long tick = (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;//- 8 * 60 * 60;
        //    //return GetBytes(tick);
        //}
        //public static long DateToUTCTime(DateTime dt)
        //{
        //    double dResult = 0;
        //    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        //    dResult = (dt - startTime).TotalSeconds;
        //    return (long)dResult;
        //    /*旧*/
        //    //long tick = (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;//- 8 * 60 * 60;
        //    //return GetBytes(tick);
        //}
        //public static DateTime UTCToDate(long tick)
        //{
        //    DateTime dt;
        //    try
        //    {
        //        //tick += 8 * 60 * 60;//东八区
        //        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        //        startTime = startTime.AddSeconds(tick);
        //        dt = startTime;
        //    }
        //    catch
        //    {
        //        dt = default(DateTime);
        //    }

        //    /*旧*/
        //    //tick = (tick + 8 * 60 * 60) * 10000000 + 621355968000000000;
        //    //DateTime dt;
        //    //try
        //    //{
        //    //    //dt = new DateTime(tick);
        //    //}
        //    //catch
        //    //{
        //    //    dt = Variable.FAIL_DATETIME;
        //    //}
        //    return dt;
        //}
        #endregion

        #region  填充数据
        public static byte[] PaddingWithLength(byte[] data, int iLenght, byte pad = 0)
        {
            byte[] rspData = new byte[iLenght];
            for (int i = 0; i < iLenght; i++)
            {
                if (data.Length <= i)
                {
                    rspData[i] = pad;
                }
                else
                {
                    rspData[i] = data[i];
                }
            }
            return rspData;
        }
        #endregion
    }
}
