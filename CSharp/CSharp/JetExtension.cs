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
            return services.AddSingleton<Jet502>();
        }
    }
}
