using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Ping
{
    public class IcmPacket
    {
        private byte type;
        private byte subCode;
        private ushort checkSum;
        private ushort identifier;
        private ushort sequenceNumber;
        private byte[] data;

        public ushort CheckSum
        {
            get
            {
                return checkSum;
            }
            set
            {
                checkSum = value;
            }
        }

        public IcmPacket(byte type, byte subCode, ushort checkSum, ushort identifier, ushort sequenceNumber, int dataSize)
        {
            this.type = type;
            this.subCode = subCode;
            this.checkSum = checkSum;
            this.identifier = identifier;
            this.sequenceNumber = sequenceNumber;
            this.data = new byte[dataSize];
            for (int i = 0; i < dataSize; i++)
            {
                data[i] = (byte)'k';
            }
        }

        public int CountByte(byte[] buffer)
        {
            var type = new [] { this.type };
            var code = new[] { this.subCode };
            var cksum = BitConverter.GetBytes(this.checkSum);
            var id = BitConverter.GetBytes(this.identifier);
            var seq = BitConverter.GetBytes(this.sequenceNumber);
            int i = 0;
            Array.Copy(type, 0, buffer, i, type.Length);
            i += type.Length;
            Array.Copy(code, 0, buffer, i, code.Length);
            i += code.Length;
            Array.Copy(cksum, 0, buffer, i, cksum.Length);
            i += cksum.Length;
            Array.Copy(id, 0, buffer, i, id.Length);
            i += id.Length;
            Array.Copy(seq, 0, buffer, i, seq.Length);
            i += seq.Length;
            Array.Copy(data, 0, buffer, i, data.Length);
            i += data.Length;
            return i;
        }

        public static ushort SumOfCheck(ushort[] buffer)
        {
            int cksum = 0;
            for (int i = 0; i < buffer.Length; i++)
                cksum += (int)buffer[i];
            cksum = (cksum >> 16) + (cksum & 0xffff);
            return (ushort)(~cksum);
        }
    }
}
