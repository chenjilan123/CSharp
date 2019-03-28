using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.IO
{
    public class AsyncIO
    {
        private const int Buffer_Size = 1024;
        public AsyncIO Run()
        {
            return this.Http().Result;
        }

        #region FileIO
        public async Task<AsyncIO> FileIO()
        {
            //Create Using I/O Thread
            using (var stream = File.Create("test1.txt", Buffer_Size, FileOptions.Asynchronous))
            using (var sw = new StreamWriter(stream))
            {
                Console.WriteLine($"Is Async: {stream.IsAsync}");
                await sw.WriteAsync(CreateFileContent());
            }
            Console.WriteLine("End.");

            return this;
        }

        private string CreateFileContent()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 100000; i++)
            {
                sb.Append($"{new Random(i).Next(0, 99999)}");
                sb.AppendLine();
            }
            return sb.ToString();
        }
        #endregion

        #region Http
        public async Task<AsyncIO> Http()
        {
            var server = new AsyncHttpServer(8095);
            var t1 = server.Start();

            await GetResponseAsync("http://localhost:8095");

            Console.WriteLine("Press enter to quie http server.");
            Console.ReadLine();
            await server.Stop();
            Console.WriteLine("");

            return this;
        }

        private async Task GetResponseAsync(string url)
        {
            //HttpClient 
            //  可以使用Get, Post, Put, Delete等请求.
            //  可以采用不同的数据格式(Json, Xml)
            //  可以指定代理服务器地址
            //  可以认证
            //  等...
            using (var client = new HttpClient())
            {
                //client.PostAsync
                //client.DeleteAsync
                //client.PutAsync

                HttpResponseMessage responseMessage = await client.GetAsync(url);

                //Header
                string responseHeaders = responseMessage.Headers.ToString();
                //Body
                string response = await responseMessage.Content.ReadAsStringAsync();

                Console.WriteLine();
                Console.WriteLine($"Response headers: \r\n{responseHeaders}");
                Console.WriteLine($"   Response body: \r\n{response}");
            }
        }
        #endregion

        #region AsyncDatabase
        public async Task<AsyncIO> AsyncDatabase()
        {

            return this;
        }

        private async Task ProcessAsynchronousIO(string dbName)
        {
            const string ConnStr = "Data Source=;Initial Catalog=;Integrated Security=True";
            string outputFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dbFileName = Path.Combine(outputFolder, $"{dbName}.mdf");
            string dbLogFileName = Path.Combine(outputFolder, $"{dbName}.ldf");

        }
        #endregion

        #region Test
        public AsyncIO Test()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{new Random(50).Next(0, 50)}");
            }
            return this;
        }
        #endregion
    }
}
