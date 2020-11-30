using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Firebus.Client
{
    public static class FirebusClientExtensions
    {
        public static IServiceCollection AddFirebusClient(this IServiceCollection services,
            Action<FirebusClientOptionsBuilder> setupAction)
        {
            var builder = new FirebusClientOptionsBuilder();

            setupAction(builder);

            services.AddSingleton(builder.Options);
            services.AddScoped<FirebusClient>();

            return services;
        }
    }
}
