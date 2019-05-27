using CSharp.Framework.Transfer.JsonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CSharp.Framework.Transfer
{
    /// <summary>
    /// 广东省公务车数据转发
    /// </summary>
    public class AnalysisGDOCPosInfo
    {
        // 平台接入信息
        private const int PlatformID = 2;
        private const string Host = "rygwctest.xunxintech.com:38025";
        private const string IV = "GjpNc29Q";

        // 组织信息
        private const string OrgID = "6";
        private const string OrgName = "公务车转发测试组";
        private const string AreaCode = "440000";
        private const string AreaName = "广东省";
        private const int AreaLevel = 1;

        // 车辆信息
        private const string PlatNum = "闽A55555";
        private const string TerminalID = "1vJ5xG2";
        private const string SimNum = "13155555222";

        private bool _bIsInitSender = false;
        private GDOCSender<GDOCPos, GDOCResult> posSender = null;
        private GDOCSender<GDOCVehicle, GDOCVehicleResult> vehicleSender = null;
        private GDOCSender<GDOCOrg, GDOCResult> orgSender = null;
        private GDOCSender<GDOCEmpty, GDOCResult> modelGetter = null;
        private GDOCSender<GDOCEmpty, GDOCResult> typeGetter = null;

        #region 工作
        public void OnWork()
        {
            try
            {
                if (!_bIsInitSender)
                {
                    InitSender();
                    _bIsInitSender = true;
                }
                //转发组织结构
                //OK
                //OrgInfoTransmit();
                ////转发车辆信息
                VehicleInfoTransmit();
                ////转发位置数据
                //PositionTransmit();

                //GetTaxiModel();
                //GetTaxiType();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void GetTaxiModel()
        {
            modelGetter.Send();
        }

        private void GetTaxiType()
        {
            typeGetter.Send();
        }
        #endregion

        #region 初始化发送者
        private void InitSender()
        {
            try
            {
                //Http位置数据发送者
                posSender = new GDOCSender<GDOCPos, GDOCResult>();
                posSender.Url = string.Format("http://{0}/Management/{1}", Host, GDOCTypeList.LocationDataManagement);
                posSender.Cmd = nameof(GDOCCmdList.UploadLocations);
                posSender.IV = IV;
                posSender.PlatformID = PlatformID;
                //Http车辆数据发送者
                vehicleSender = new GDOCSender<GDOCVehicle, GDOCVehicleResult>();
                vehicleSender.Url = string.Format("http://{0}/Management/{1}", Host, GDOCTypeList.BasicDataManagement);
                vehicleSender.Cmd = nameof(GDOCCmdList.VehicleInfos);
                vehicleSender.IV = IV;
                vehicleSender.PlatformID = PlatformID;
                //Http组织结构发送者
                orgSender = new GDOCSender<GDOCOrg, GDOCResult>();
                orgSender.Url = string.Format("http://{0}/Management/{1}", Host, GDOCTypeList.BasicDataManagement);
                orgSender.Cmd = nameof(GDOCCmdList.OrganizationInfo);
                orgSender.IV = IV;
                orgSender.PlatformID = PlatformID;

                //获取车辆品牌
                modelGetter = new GDOCSender<GDOCEmpty, GDOCResult>();
                modelGetter.Url = string.Format("http://{0}/Management/{1}", Host, GDOCTypeList.BasicDataManagement);
                modelGetter.Cmd = nameof(GDOCCmdList.GetTaxiModel);
                modelGetter.IV = IV;
                modelGetter.PlatformID = PlatformID;

                //获取订单车型
               typeGetter = new GDOCSender<GDOCEmpty, GDOCResult>();
               typeGetter.Url = string.Format("http://{0}/Management/{1}", Host, GDOCTypeList.BasicDataManagement);
               typeGetter.Cmd = nameof(GDOCCmdList.GetTaxiType);
               typeGetter.IV = IV;
               typeGetter.PlatformID = PlatformID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region 转发组织结构
        /// <summary>
        /// 转发组织结构
        /// </summary>
        private void OrgInfoTransmit()
        {
            var orgList = GetOrgInfo();
            if (orgList.Count <= 0)
            {
                return;
            }
            foreach (var org in orgList)
            {
                orgSender.Send(org);
            }
        }

        private List<GDOCOrg> GetOrgInfo()
        {
            return new List<GDOCOrg>()
            {
                new GDOCOrg()
                {
                    PlatformID = PlatformID,
                    OrgID = OrgID,
                    Name = OrgName,
                    ShortName = "",
                    Phone = "13123456789",
                    Memo = "",
                    AreaCode = AreaCode,
                    AreaName = AreaName,
                    AreaLevel = AreaLevel,
                    Seq = 2,
                    Flag = 1,
                },
            };
        }
        #endregion

        #region 转发车辆信息
        /// <summary>
        /// 转发车辆信息
        /// </summary>
        private void VehicleInfoTransmit()
        {
            var vehicleList = GetVehicleInfo();
            if (vehicleList.Count <= 0)
            {
                return;
            }
            vehicleSender.Send(vehicleList);
        }

        /// <summary>
        /// 获取车辆基本信息
        /// </summary>
        /// <returns></returns>
        public List<GDOCVehicle> GetVehicleInfo()
        {
            var vehicleList = new List<GDOCVehicle>();
            GDOCVehicle vehicle = new GDOCVehicle()
            {
                PlateNo = PlatNum,
                PlatformID = PlatformID,
                TaxiModelId = "0", //车辆品牌, 平台有
                OrgID = OrgID,
                VehicleDimensions = string.Empty,
                VinNo = string.Empty, //平台有
                EngineNo = string.Empty, //平台有
                Displacement = 0.0F,
                Color = "Z",
                GearBox = 1,    
                LaunchDate = DateTime.MinValue.ToString("yyyyMMdd"),
                ReleaseStatus = 1,
                TerminalId = GetTerminalID(TerminalID),
                POVehicleUseNature = "6",
                TaxiTypeCode = 0, 
                MaxPassengerNumber = 0,
                PassengerNumber = 0,
                Identification = 0,
                IsAccessPlatform = 1,
                VehicleStatus = 0,
                TerminalInstallationDate = DateTime.MinValue.ToString("yyyyMMdd"), //平台有

                AllocationUnit = "0",
                AllocationNumber = "0",
                Supplier = "0",
                DrivingLicence = "0",       //平台有
                Owner = "0",                //平台有
                ContactName = "阳顶天", //联系人姓名
                OfficePhone = "0", //
                Department = "0",
                ContactMobile = "15000000000", //联系人手机
                duty = "0",
                Fax = "0",

                Flag = 2,
            };
            vehicleList.Add(vehicle);
            return vehicleList;
        }

        private string GetTerminalID(string terminalID)
        {
            return string.Format("20{0}{1}", PlatformID.ToString().PadLeft(2, '0'), terminalID.PadLeft(8, '0').Substring(0, 8));
        }
        #endregion

        #region 转发位置数据
        /// <summary>
        /// 转发位置数据
        /// </summary>
        private void PositionTransmit()
        {
            var posList = new List<GDOCPos>();
            var pos = new GDOCPos()
            {
                TerminalId = "20X450012",//TerminalID,
                PlateNo = "闽B54A43",//PlatNum,
                Longitude = 117.501231,
                Latitude = 27.405012,
                LocTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Speed = 60.8F,
                Direction = 180,
                SubMileage = 50.41,
                IsPosition = 1,
                InputMap = 1,
            };
            posList.Add(pos);
            posSender.Send(posList);
        }
        #endregion 
    }
}
