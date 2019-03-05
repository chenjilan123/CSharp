using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = CreateServices()
                .AddVehicle();

            using (var provider = services.BuildServiceProvider())
            {
                try
                {
                    var jet = provider.GetService<Jet502>();
                    jet.Fly();
                    jet.Attack();
                    jet.Information();

                    var airport = provider.GetService<Airport>();
                    airport.Run();

                    var bus = provider.GetService<IAirbus>();
                    Console.WriteLine(bus.Id);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.ToString());
                }
            }
        } 


        private static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection()
                .AddLogging(cfg => cfg.SetMinimumLevel(LogLevel.Trace))
                //.AddOptions()
                .Configure<JetOptions>(opt =>
                {
                    opt.Id = 526;
                    opt.OilBox = "FHK7";
                    opt.Code = "JH70";
                })
                .Configure<AirportOptions>(opt =>
                {
                    opt.Area = 98.50;
                    opt.Capacity = 150;
                    opt.Code = "UE5M3";
                });
            //.Configure<T2>(opt2 => ;)

            return services;
        }
    }
}
