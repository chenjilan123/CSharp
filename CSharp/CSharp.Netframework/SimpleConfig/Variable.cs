using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.SimpleConfig
{
    public static class Variable
    {
        [CustomAttribute(SysConfigType.LocalConfigXml, VariableDataType.TString)]
        public static string SysTitle = "";
        [CustomAttribute(SysConfigType.LocalConfigXml, VariableDataType.TBool)]
        public static bool IsOpenAlarm = false;
        [CustomAttribute(SysConfigType.LocalConfigXml, VariableDataType.TInt)]
        public static int Value = 95599;

    }
}
