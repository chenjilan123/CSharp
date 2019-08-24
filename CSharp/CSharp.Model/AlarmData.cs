using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Model
{
    public class AlarmData
    {
        /// <summary>
        /// 报警名称
        /// </summary>
        public string AlarmName { get; set; }
        /// <summary>
        /// 报警标识
        /// </summary>
        public long AlarmFlag { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNum { get; set; }
        /// <summary>
        /// 通讯号
        /// </summary>
        public string SimNum { get; set; }
        /// <summary>
        /// 报警持续时间(秒)
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 报警起始时间
        /// </summary>
        public DateTime BeginAlarmTime { get; set; }
        /// <summary>
        /// 报警结束时间
        /// </summary>
        public DateTime EndAlarmTime { get; set; }
        /// <summary>
        /// 报警起始经度
        /// </summary>
        public double BeginLongitude { get; set; }
        /// <summary>
        /// 报警起始纬度
        /// </summary>
        public double BeginLatitude { get; set; }
        /// <summary>
        /// 报警结束经度
        /// </summary>
        public double EndLongitude { get; set; }
        /// <summary>
        /// 报警结束纬度
        /// </summary>
        public double EndLatitude { get; set; }
    }
}
