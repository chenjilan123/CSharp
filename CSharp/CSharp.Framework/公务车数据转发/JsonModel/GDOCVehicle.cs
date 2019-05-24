using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Framework.Transfer.JsonModel
{
    public class GDOCVehicle
    {
        /// <summary>
        /// 车牌号码
        /// </summary>
        public string PlateNo { get; set; }
        /// <summary>
        /// 地市公车平台账号代码
        /// </summary>
        public int PlatformID { get; set; }
        /// <summary>
        /// 车辆品牌ID(平台统一分配) GetTaxiMode
        /// </summary>
        public string TaxiModelId { get; set; }
        /// <summary>
        /// 地市自建系统唯一代码
        /// </summary>
        public string OrgID { get; set; }
        /// <summary>
        /// 车身尺寸
        /// </summary>
        public string VehicleDimensions { get; set; }
        /// <summary>
        /// 车架号
        /// </summary>
        public string VinNo { get; set; }
        /// <summary>
        /// 发动机号
        /// </summary>
        public string EngineNo { get; set; }
        /// <summary>
        /// 排量
        /// </summary>
        public float Displacement { get; set; }
        public string Color { get; set; } //车身颜色代码(GA28.5-2005): A白B灰C黄D粉E红F紫G绿H蓝I棕J黑Z其他
        /// <summary>
        /// 波箱(选填)
        /// </summary>
        public int GearBox { get; set; }
        /// <summary>
        /// 购车日期(选填)
        ///     格式：yyyyMMddHHmmss
        /// </summary>
        public string LaunchDate { get; set; }
        /// <summary>
        /// 车辆绑定状态
        ///     0-未绑定终端, 1-已绑定终端
        /// </summary>
        public int ReleaseStatus { get; set; }
        /// <summary>
        /// 终端ID
        /// </summary>
        public string TerminalId { get; set; }
        /// <summary>
        /// 车辆使用性质, 多于1个时, 用分号隔开, 例如: 1;2;
        ///     1 机要通信用车； 2应急用车(重大)；3执法勤用车； 4特种专业技术用车(非执法执勤);5 行政执法执勤； 6其他
        /// </summary>
        public string POVehicleUseNature { get; set; }
        /// <summary>
        /// 订单车型代码
        /// </summary>
        public int TaxiTypeCode { get; set; }
        /// <summary>
        /// 核载人数
        /// </summary>
        public int MaxPassengerNumber { get; set; }
        /// <summary>
        /// 载客数量
        /// </summary>
        public int PassengerNumber { get; set; }
        /// <summary>
        /// 标识化
        ///     1-是, 0-否
        /// </summary>
        public int Identification { get; set; }
        /// <summary>
        /// 是否接入平台
        ///     1-是, 0-否
        /// </summary>
        public int IsAccessPlatform { get; set; }
        /// <summary>
        /// 车辆现状
        ///     1-在用, 0-空置
        /// </summary>
        public int VehicleStatus { get; set; }
        /// <summary>
        /// 定编单位
        /// </summary>
        public string AllocationUnit { get; set; }
        /// <summary>
        /// 定编证号
        /// </summary>
        public string AllocationNumber { get; set; }
        /// <summary>
        /// 终端安装日期
        /// </summary>
        public string TerminalInstallationDate { get; set; }
        /// <summary>
        /// 终端设备供应商
        /// </summary>
        public string Supplier { get; set; }
        /// <summary>
        /// 车辆行驶证
        /// </summary>
        public string DrivingLicence { get; set; }
        /// <summary>
        /// 车辆所有人
        /// </summary>
        public string Owner { get; set; }
        ///// <summary>
        ///// 累计行驶公里数(公里)
        ///// </summary>
        //public float AccumulatedMileage { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string ContactMobile { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string duty { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        ///// <summary>
        ///// 任务状态
        ///// </summary>
        //public int TaskStatus { get; set; }
        ///// <summary>
        ///// 调度模式
        ///// </summary>
        //public int DispatchVehicleMode { get; set; }
        ///// <summary>
        ///// 可驾驶驾驶证类型
        ///// </summary>
        //public string LicenseType { get; set; }
        /// <summary>
        /// 标签(1 新增; 2 修改; 3 删除)
        /// </summary>
        public int Flag { get; set; }
    }
}
