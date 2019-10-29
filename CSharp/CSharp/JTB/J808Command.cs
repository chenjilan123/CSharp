using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.JTB
{
    public class J808Command : ICommand
    {
        private static readonly Encoding Gb2312 = Encoding.GetEncoding("gb2312");

        private readonly List<FieldDescription> Fields;
        public ushort Id { get; set; }
        public ushort Property { get; set; }
        public string Phone { get; set; }
        public ushort OrderId { get; set; }

        public J808Command()
        {
            Property = 0x8010;
            Phone = "13800000050";
            Fields = new List<FieldDescription>();
        }

        public void AppendField(string name, string value)
        {
            Fields.Add(new FieldDescription() { Name = name, Value = value });
        }

        private string GetHeaderDescription()
        {
            var p0 = (byte)(Property & 0x000F);
            var p1 = (byte)((Property & 0x00F0) >> 4);
            var p2 = (byte)((Property & 0x0F00) >> 8);
            var p3 = (byte)((Property & 0xF000) >> 12);
            return
                $"|--------------------------------------------------------------------|" +
                Environment.NewLine +
                $"| 消息ID " +
                $"| 消息体属性 15   11   7    3   " +
                $"|       手机号 " +
                $"| 消息流水号 " +
                $"|" +
                Environment.NewLine +
                $"| 0x{Id.ToString("X4")} " +
                $"|            {Convert.ToString(p3, 2).PadLeft(4, '0')} {Convert.ToString(p2, 2).PadLeft(4, '0')} {Convert.ToString(p1, 2).PadLeft(4, '0')} {Convert.ToString(p0, 2).PadLeft(4, '0')}" +
                $"| {Phone.ToString().PadLeft(12, '0')} " +
                $"|      {OrderId.ToString().PadLeft(5, ' ')} " +
                $"|" +
                Environment.NewLine +
                $"|--------------------------------------------------------------------|" + 
                Environment.NewLine;
        }

        public string GetDescription()
        {
            var header = GetHeaderDescription();
            var sb = new StringBuilder(header);
            sb.Append($"|  数据体                                                            |{Environment.NewLine}"
                + "|--------------------------------------------------------------------|" +
            Environment.NewLine);
            foreach (var field in Fields)
            {
                var nameLen = Gb2312.GetBytes(field.Name).Length;
                var valueLen = Gb2312.GetBytes(field.Value).Length;
                var name = field.Name.PadLeft(28 - nameLen + field.Name.Length);
                var value = field.Value.PadRight(38 - valueLen + field.Value.Length);
                var l1 = name.Length;
                sb.Append($"|{name}: {value}|{Environment.NewLine}");
            }

            sb.Append($"|--------------------------------------------------------------------|" +
            Environment.NewLine);
            return sb.ToString();
        }
    }

    public struct FieldDescription
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
