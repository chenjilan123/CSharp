using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.Model;

namespace CSharp.Client.HEZD
{
    public interface IHEZDSender
    {
        void PostAlarm(List<AlarmData> alarmLst);
    }
}
