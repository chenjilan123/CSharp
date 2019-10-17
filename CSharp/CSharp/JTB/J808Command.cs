using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.JTB
{
    public class J808Command : ICommand
    {
        public ushort Id { get; set; }
        public ushort Property { get; set; }
        public string Phone { get; set; }
        public ushort OrderId { get; set; }
        private string GetHeaderDescription()
        {
            Property = 0x8010;
            Phone = "13800000050";
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
                $"|--------------------------------------------------------------------|";
        }
        public string GetDescription()
        {
            return GetHeaderDescription();
        }
    }
}
