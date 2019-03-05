using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public static class JetExtension
    {
        public static IServiceCollection AddJet(this IServiceCollection services)
        {
            //services.AddOptions<JetOptions>();
            services.AddSingleton<Jet502>();
            //    .Decorate<Jet502>((jet, _) =>
            //{
            //    jet.Missiles = 50;
            //    return jet;
            //});
            return services;
        }
    }
}
