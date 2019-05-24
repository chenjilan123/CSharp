using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Framework.Transfer.JsonModel
{
    public class GDOCOrg
    {
        public int PlatformID { get; set; }
        public string OrgID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Phone { get; set; }
        public string Memo { get; set; }
        //public int IsSMSWarning { get; set; }
        //public string WarningPhones { get; set; }
        //public int IsSupervision { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public int AreaLevel { get; set; }
        public int Seq { get; set; }
        public int Flag { get; set; }
    }
}
