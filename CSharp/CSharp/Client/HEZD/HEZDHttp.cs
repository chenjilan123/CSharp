using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CSharp.Model;

namespace CSharp.Client.HEZD
{
    public class HEZDHttp : IHEZDSender
    {
        private readonly ISerializer _serializer;
        private readonly IGisService _gisService;
        public HEZDHttp(ISerializer serializer, IGisService gisService)
        {
            this._serializer = serializer;
            this._gisService = gisService;
        }
        public void PostAlarm(List<AlarmData> alarmLst)
        {
            var postLst = alarmLst.Select(a => new
            {
                title = a.AlarmName,
                carNo = a.PlateNum,
                duration = a.Duration,
                startTime = a.BeginAlarmTime.ToString("yyyy-MM-dd HH:mm:ss"),
                endTime = a.EndAlarmTime.ToString("yyyy-MM-dd HH:mm:ss"),
                startPos = _gisService.GetPosition(a.BeginLongitude, a.BeginLatitude),
                endPos = _gisService.GetPosition(a.EndLongitude, a.EndLatitude),
            });
            var sJson = _serializer.Serialize(postLst);
            Console.WriteLine("发送Json字符串：");
            Console.WriteLine(sJson);
        }
    }
}
