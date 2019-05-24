using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Framework.Transfer.JsonModel
{
    public class GDOCPos
    {
        public string TerminalId { get; set; }
        public string PlateNo { get; set; }
        /// <summary>
        /// 经度: WGS84格式
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度: WGS84格式
        /// </summary>
        public double Latitude { get; set; }
        public string LocTime { get; set; }
        public float Speed { get; set; }
        public float Direction { get; set; }
        public double SubMileage { get; set; }
        public int IsPosition { get; set; }
        public int InputMap { get; set; }
    }
}
