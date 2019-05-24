using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Framework.Transfer
{
    public enum GDOCCmdList
    {
        VehicleInfos = 1,   //车辆信息(批量)
        UploadLocations = 2, //位置数据接入(批量)
        OrganizationInfo = 3, //组织机构信息

        GetTaxiModel = 4, //获取车辆品牌
        GetTaxiType = 5, //获取订单车型
    }
}
