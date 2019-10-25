using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Lib
{
    public class BinaryHelper
    {
        ////十六进制字符串转字节数组
        public static byte[] HexStrToBytes(string value)
        {
            int len = value.Length / 2;
            byte[] ret = new byte[len];
            for (int i = 0; i < len; i++)
                ret[i] = (byte)(Convert.ToInt32(value.Substring(i * 2, 2), 16));
            return ret;
        }
    }
}
