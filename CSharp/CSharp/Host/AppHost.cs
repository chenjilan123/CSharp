using CSharp.Option;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Host
{
    public class AppHost
    {
        #region ConfigureHost
        public static async Task ConfigureHost()
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("Config/appSetting.json", optional: false, reloadOnChange: true);

                    var configuration = config.Build();
                })
                .ConfigureServices((context, services) =>
                {
                    //Config options
                    services.Configure<InternalOption>(context.Configuration.GetSection(nameof(InternalOption)));

                    //Add services to start
                    services.AddHostedService<InternalService>();
                });
            var host = builder.Build();
            await host.RunAsync();
        }
        #endregion
    }
}
