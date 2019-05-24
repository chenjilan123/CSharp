using System;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;

namespace CSharp.Framework.Helper
{
    /// <summary>
    /// 提供加密和解密接口
    /// </summary>
    public class SecurityHelper
    {
        private static string _sKey = string.Empty;
        
        /// <summary>
        /// 加密公钥
        /// </summary>
        public static string Key
        {
            get
            {
                if(string.IsNullOrEmpty(_sKey))
                {
                    // 创建Key
                    _sKey = GenerateKey();
                }
                return _sKey; 
            }
        }

        /// <summary>
        ///  创建Key
        /// </summary>
        private static string GenerateKey()
        {
            string sKey = string.Empty;

            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            sKey = ASCIIEncoding.ASCII.GetString(desCrypto.Key);

            while (sKey.Length > 0 && sKey.IndexOf('|') != -1)
            {
                desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
                sKey = ASCIIEncoding.ASCII.GetString(desCrypto.Key);
            }

            return sKey;
        }
        /// <summary>
        /// 加密字符串
        /// </summary>
        public static string EncryptString(string inputString)
        {
            byte[] data = Encoding.UTF8.GetBytes(inputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(_sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(_sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);

            return BitConverter.ToString(result);
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        public static string DecryptString(string inputString)
        {
            string[] sInput = inputString.Split("-".ToCharArray());
            byte[] data = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(_sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(_sKey);
            ICryptoTransform desencrypt = DES.CreateDecryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);

            return Encoding.UTF8.GetString(result);
        }
        /// <summary>
        /// MD5加密,主要用于密码加密。32位
        /// </summary>
        public static string EncryptMD5String(string inputString)
        {
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.ASCII.GetBytes(inputString);
            byte[] hash = MD5.ComputeHash(bytValue);
            return BCDToString(hash,0,hash.Length);
        }
        /// <summary>
        /// MD5 -16位
        /// </summary>
        public static string EncryptMD5String16(string inputString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string sEncrypt = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(inputString)),4, 8);
            sEncrypt = sEncrypt.Replace("-", "");
            return sEncrypt;
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
        /// <summary>
        /// HmacSha1
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] HmacSha1Sign(string text, string key)
        {
            Encoding encode = Encoding.GetEncoding("utf-8");
            byte[] byteData = encode.GetBytes(text);
            byte[] byteKey = encode.GetBytes(key);
            HMACSHA1 hmac = new HMACSHA1(byteKey);
            CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write);
            cs.Write(byteData, 0, byteData.Length);
            cs.Close();
            return hmac.Hash;
        }

        public static string EncryptDesString(string text, byte[] key, byte[] iv)
        {
            DES des = new DESCryptoServiceProvider();
            des.Key = key;
            des.IV = iv;
            var textBytes = Encoding.UTF8.GetBytes(text);
            var sbResult = new StringBuilder();
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(textBytes, 0, textBytes.Length);
                cs.FlushFinalBlock();
                foreach (var b in ms.ToArray())
                {
                    sbResult.AppendFormat("{0:X2}", b);
                }
            }
            return sbResult.ToString();
        }

        //public static string EncryptDesString(string text, byte[] key, byte[] iv)
        //{
        //    DES des = new DESCryptoServiceProvider();
        //    des.Key = key;
        //    des.IV = iv;
        //    var textBytes = Encoding.UTF8.GetBytes(text);
        //    using (var ms = new MemoryStream())
        //    using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
        //    {
        //        cs.Write(textBytes, 0, textBytes.Length);
        //        cs.FlushFinalBlock();
        //        return Convert.ToBase64String(ms.ToArray());
        //    }
        //}

        //public static string EncryptDesString(string text, byte[] key, byte[] iv)
        //{
        //    DES des = new DESCryptoServiceProvider();
        //    des.Key = key;
        //    des.IV = iv;
        //    var textBytes = Encoding.UTF8.GetBytes(text);
        //    using (var ms = new MemoryStream())
        //    using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
        //    {
        //        cs.Write(textBytes, 0, textBytes.Length);
        //        cs.FlushFinalBlock();
        //        return Encoding.UTF8.GetString(ms.ToArray());
        //    }
        //}
    }
}
