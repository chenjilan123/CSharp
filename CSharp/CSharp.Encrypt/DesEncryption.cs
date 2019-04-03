using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CSharp.Encrypt
{
    public class DesEncryption
    {
        static DES _des = null;
        static DesEncryption()
        {
            _des = new DESCryptoServiceProvider();
        }
        public byte[] GetKey()
        {
            _des.GenerateKey();
            return _des.Key;
        }
        public string Encrypt(string text, byte[] key, byte[] iv)
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
    }
}
