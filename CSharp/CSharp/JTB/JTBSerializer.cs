using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.JTB
{
    public class JTBSerializer
    {
        private Encoding Gb2312 = Encoding.GetEncoding("GB2312");

        public ICommand Deserialize(J808Command command, IEnumerable<byte> data)
        {
            var commandResolver = JTB.J808ProtocolRulerProvider.Instance.GetJ808V2013Command(command.Id);
            var enumerator = data.GetEnumerator();
            foreach (var field in commandResolver.Fields)
            {
                //Type type;
                object value;
                switch (field.Type)
                {
                    case "BYTE":
                        //type = typeof(byte);
                        value = GetByte(enumerator);
                        break;
                    case "WORD":
                        //type = typeof(ushort);
                        value = GetWord(enumerator);
                        break;
                    case "DWORD":
                        //type = typeof(uint);
                        value = GetDword(enumerator);
                        break;
                    case "STRING":
                        //type = typeof(string);
                        value = GetString(enumerator, field.Length);
                        break;
                    case "BYTE[n]":
                        //type = typeof(byte[]);
                        value = GetByteString(enumerator, field.Length);
                        break;
                        //break;
                    case "BCD[n]":
                        value = GetBCDString(enumerator, field.Length);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                command.AppendField(field.Name, value.ToString());
            }
            return command;
        }


        private void TryMoveNext(IEnumerator<byte> enumerator)
        {
            if (!enumerator.MoveNext()) throw new IndexOutOfRangeException();
        }

        private byte GetByte(IEnumerator<byte> enumerator)
        {
            enumerator.MoveNext();
            return enumerator.Current;
        }

        private ushort GetWord(IEnumerator<byte> enumerator)
        {
            var data = new byte[2];
            for (int i = 0; i < data.Length; i++)
            {
                TryMoveNext(enumerator);
                data[i] = enumerator.Current;
            }
            return BitConverter.ToUInt16(data);
        }

        private uint GetDword(IEnumerator<byte> enumerator)
        {
            var data = new byte[4];
            for (int i = 0; i < data.Length; i++)
            {
                TryMoveNext(enumerator);
                data[i] = enumerator.Current;
            }
            return BitConverter.ToUInt32(data);
        }

        private string GetString(IEnumerator<byte> enumerator, uint length)
        {
            if (length == 0)
            {
                TryMoveNext(enumerator);
                length = (uint)enumerator.Current;
            }
            var data = new byte[length];
            for (int i = 0; i < data.Length; i++)
            {
                TryMoveNext(enumerator);
                data[i] = enumerator.Current;
            }
            return Gb2312.GetString(data);
        }

        private string GetByteString(IEnumerator<byte> enumerator, uint length)
        {
            if (length == 0)
            {
                TryMoveNext(enumerator);
                length = (uint)enumerator.Current;
            }
            var data = new byte[length];
            for (int i = 0; i < data.Length; i++)
            {
                TryMoveNext(enumerator);
                data[i] = enumerator.Current;
            }
            return Encoding.ASCII.GetString(data);
        }
        private string GetBCDString(IEnumerator<byte> enumerator, uint length)
        {
            if (length == 0)
            {
                TryMoveNext(enumerator);
                length = (uint)enumerator.Current;
            }
            var data = new byte[length];
            for (int i = 0; i < data.Length; i++)
            {
                TryMoveNext(enumerator);
                data[i] = enumerator.Current;
            }
            var sb = new StringBuilder(data.Length);
            foreach (var b in data)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }
}
