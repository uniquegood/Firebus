using System;
using System.Collections.Generic;
using System.Text;
using Firebus.Server;
using Microsoft.Extensions.DependencyInjection;

namespace Firebus.Manage
{
    public static class FirebusManageExtensions
    {
        public static IServiceCollection AddFirebusManage(this IServiceCollection services,
            Action<FirebusManageOptionsBuilder> setupAction)
        {
            var builder = new FirebusManageOptionsBuilder();

            setupAction(builder);

            services.AddSingleton(builder.Options);
            services.AddScoped<FirebusJobManageService>();

            return services;
        }
    }
}
