using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Framework.Face.V2_2.Data
{
    public struct ASF_ActiveFileInfo
    {
        public string startTime;
        public string endTime;
        public string platform;
        public string sdkType;
        public string appId;
        public string sdkKey;
        public string sdkVersion;
        public string fileVersion;

        internal void PrintInfo()
        {
            Console.WriteLine("激活文件信息: ");
            var tStart = new DateTime(1970, 1, 1).AddSeconds(int.Parse(startTime));
            var tEnd = new DateTime(1970, 1, 1).AddSeconds(int.Parse(endTime));
            Console.WriteLine($"\t  有效期: {tStart.ToString("yyyy-MM-dd HH:mm:ss")} - {tEnd.ToString("yyyy-MM-dd HH:mm:ss")}");
            Console.WriteLine($"\t平台类型: {platform}");
            Console.WriteLine($"\t SDK类型: {sdkType}");
            Console.WriteLine($"\t   AppId: {appId}");
            Console.WriteLine($"\t  SDKKey: {sdkKey}");
            Console.WriteLine($"\t SDK版本: {sdkVersion}");
            Console.WriteLine($"\t文件版本: {fileVersion}");

        }
    }
}
