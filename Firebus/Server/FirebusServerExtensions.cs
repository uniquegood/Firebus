using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Firebus.Server
{
    public static class FirebusServerExtensions
    {
        public static IServiceCollection AddFirebusServer(this IServiceCollection services,
            Action<FirebusServerOptionsBuilder> setupAction)
        {
            var builder = new FirebusServerOptionsBuilder();

            setupAction(builder);

            services.AddSingleton(builder.Options);
            services.AddSingleton<FirebusJobHandler>();
            services.AddScoped<JobExecutionContextAccessor>();

            return services;
        }

        public static IApplicationBuilder UseFirebusServer(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<FirebusServerOptions>();
            var jobHandler = app.ApplicationServices.GetService<FirebusJobHandler>();

            foreach (var receiver in options.JobReceivers)
            {
                receiver.RegisterJobHandler(jobHandler);
                receiver.BeginReceive();
            }

            return app;
        }
    }
}
