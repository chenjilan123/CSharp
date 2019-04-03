using CSharp.Encrypt;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace CSharp.xUnitTest
{
    public class Des
    {
        [Fact]
        public void Encrypt()
        {
            var des = new DesEncryption();
            //var s = des.Encrypt("Hello Hedshgfgsdgfdsgdfghrthrthggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg", "13105011", "x4x2Hf");

        }

        [Fact]
        public void GenerateKey()
        {
            var des = new DesEncryption();
            var keyBytes = des.GetKey();

            var sb = new StringBuilder();

            var key = Encoding.UTF8.GetString(keyBytes);
            foreach (var b in keyBytes)
            {
                sb.AppendFormat("{0:X2}", b);
            }
        }

        /// <summary>
        /// Pratice from official
        /// ref: https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.descryptoserviceprovider?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev15.query%3FappId%3DDev15IDEF1%26l%3DEN-US%26k%3Dk(System.Security.Cryptography.DESCryptoServiceProvider);k(DevLang-csharp)%26rd%3Dtrue&view=netframework-4.7.2
        /// </summary>
        /// <param name="inName"></param>
        /// <param name="outName"></param>
        /// <param name="desKey"></param>
        /// <param name="desIV"></param>
        private void EncryptData(string inName, string outName, byte[] desKey, byte[] desIV)
        {
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            byte[] bin = new byte[100];
            long rdlen = 0;
            long totlen = fin.Length;
            int len;

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

            Console.WriteLine("Encrypting...");

            while(rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen += len;
                Console.WriteLine($"{rdlen} bytes processed");

            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }
    }
}
