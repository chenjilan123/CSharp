using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.AliCloudOss
{
    public class AliYunOss
    {
        private const string endpoint = "https://oss-cn-shanghai.aliyuncs.com";
        //private const string endpoint = "oss-cn-beijing.aliyuncs.com";
        //private const string endpoint = "oss-cn-hangzhou.aliyuncs.com";
        private const string accessKeyId = "LTAIZ5yLC3bxmMqp";
        private const string accessKeySecret = "qQH2Ac5ta7aTmyrBIG6J6Q6Bh4TyyB";
        private const string bucketName = "";

        public void GetBucket()
        {
            //OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            //var buckets = client.ListBuckets();
            //foreach (var bucket in buckets)
            //{
            //    Console.WriteLine(bucket.Name + ", " + bucket.Location + ", " + bucket.Owner);
            //}

            
            OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            DateTime expiration = DateTime.Now.AddMilliseconds(3600 * 1000);
            //生成临时授权Url:
            //https://help.aliyun.com/document_detail/32016.html?spm=a2c4g.11186623.2.20.7e496aaehDImcH#concept-32016-zh
            //var url = client.GeneratePresignedUri("1011007", "api-ms-win-core-file-l1-1-0.dll", expiration);
            //var url = client.GeneratePresignedUri("1011007", "api-ms-win-core-file-l1-1-0.dll", expiration, SignHttpMethod.Put);
            Console.WriteLine(url);
        }

        public void CreateBucket()
        {
            OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            client.CreateBucket(bucketName);
        }
    }
}
