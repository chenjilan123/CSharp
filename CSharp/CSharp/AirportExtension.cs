using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public static class AirportExtension
    {
        public static IServiceCollection AddVehicle(this IServiceCollection services)
        {
            //services.AddOptions<JetOptions>();
            services.AddSingleton<Jet502>();
            services.AddSingleton<IAirbus, EnlargedAirbus>();
            services.AddSingleton<IAirbus, EnlargedAirbus>();
            services.AddSingleton<IAirbus, Airbus>(); 
            services.AddSingleton<IAirbus, EnlargedAirbus>();
            services.AddSingleton<IAirbus, Airbus>(); //First
            services.AddSingleton<Airport>();

            //Error
            //    .Decorate<Jet502>((jet, _) =>
            //{
            //    jet.Missiles = 50;
            //    return jet;
            //});
            return services;
        }
    }
}
