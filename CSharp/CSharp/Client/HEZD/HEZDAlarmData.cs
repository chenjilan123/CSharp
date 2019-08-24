using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Client.HEZD
{
    public class HEZDAlarmData
    {
        public string title { get; set; }
        public string carNo { get; set; }
        public int duration { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string startPos { get; set; }
        public string endPos { get; set; }
    }
}
