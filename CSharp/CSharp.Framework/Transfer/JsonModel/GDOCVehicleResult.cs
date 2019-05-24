using CSharp.Framework.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Framework.Transfer.JsonModel
{
    public class GDOCVehicleResult : GDOCResult
    {
        private GPDCVehicleResultData _data;
        public string Data { get; set; }

        public override string GetDescription()
        {
            if (string.IsNullOrEmpty(Data))
            {
                return string.Format("{0}, Warning: {1}", base.GetDescription(), "Data为空");
            }
            if (_data == null)
            {
                _data = SerializeHelper.JsonToObject<GPDCVehicleResultData>(Data);
            }
            return string.Format("{0}, No: {1}, Result: {2}, ErrectRows: {3}", base.GetDescription(), _data.No, _data.Result, _data.EffectRows);
        }
    }

    public class GPDCVehicleResultData
    {
        public int No { get; set; }
        public int Result { get; set; }
        public int EffectRows { get; set; }
    }
}
