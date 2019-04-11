using CSharp.Model;
using CSharp.Option;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Host
{
    public class InternalService : IHostedService
    {
        private readonly IApplicationLifetime _appLifetime;
        private readonly IConfiguration _config;
        private readonly Pilot _pilot;
        private readonly InternalOption _option;
        public InternalService(IApplicationLifetime appLifetime
            , IConfiguration config
            , IOptions<InternalOption> option)
        {
            _appLifetime = appLifetime;
            _config = config;
            _option = option.Value;

            _pilot = new Pilot();
            //config.GetSection("Pilot").Bind(_pilot);
            _pilot = config.GetSection("Pilot").Get<Pilot>();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine("InternalService started");
                Console.WriteLine($"Configuration: {_config["Level"]}");
                Console.WriteLine($"Configuration: {_config["Voice"]}");
                Console.WriteLine($"        Pilot: " + Environment.NewLine +
                                  $"               Name: {_pilot.Name}" + Environment.NewLine + 
                                  $"                 Id: {_pilot.Id}" + Environment.NewLine +
                                  $"             Weight: {_pilot.Weight}" + Environment.NewLine +
                                  $"            Checked: {_pilot.Checked}");
                Console.WriteLine();
                Console.WriteLine($"       Option: " + Environment.NewLine +
                                  $"            Message:{_option.Message}");
                Console.WriteLine();
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
