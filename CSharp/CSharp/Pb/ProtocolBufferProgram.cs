using CSharp.Entity;
using CSharp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Pb
{
    public class ProtocolBufferProgram
    {
        private const string ProvOrderCreate = "0AA8010A0C3132303044444358333330371201301881408A80048F010A0C31323030444443583333303710BAD2161A0E333532303531313532383130333620FB898A93D2CB0428FB898A93D2CB043A2EE4B88AE6B5B7E8B7AF2E7C333133E7BE8AE5BA84E99381E99485E78380E7BE8AE8828928E5A881E6B5B7E5BA972940FA8AA03A4898B2EC115212E79A87E586A0E88AB1E59BADE58C97E58CBA58F7F69F3A60DDE8EC116801720130";
        public void Run()
        {
            Deserialize();
            //var value = Google.ProtocolBuffers.CodedInputStream.DecodeZigZag32(1);
            //Console.WriteLine(value);
        }

        private void Deserialize()
        {
            var data = StringToBCD(ProvOrderCreate);

            OTIpcList listRecord; 
            listRecord = ProtoBufHelper.Deserialize<OTIpcList>(data);
            foreach (var item in listRecord.otpic)
            {
                Console.WriteLine(item.CompanyId);
                foreach (var order in item.orderCreate)
                {
                    var type = order.GetType();
                    var props = type.GetProperties();
                    foreach (var prop in props)
                    {
                        var value = prop.GetValue(order, null);
                        Console.WriteLine($"{prop.Name}: {value}");
                    }
                }
            }
            //foreach (var b in data)
            //{
            //    Console.Write(b.ToString("X2"));
            //}

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
    }
}
