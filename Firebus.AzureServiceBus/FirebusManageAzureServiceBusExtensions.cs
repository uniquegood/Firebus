using System;
using System.Collections.Generic;
using System.Text;
using Firebus.Manage;

namespace Firebus.AzureServiceBus
{
    public static class FirebusManageAzureServiceBusExtensions
    {
        public static FirebusManageOptionsBuilder UseAzureServiceBus(this FirebusManageOptionsBuilder builder,
            AzureServiceBusServerOptions options)
        {
            foreach (var queue in options.QueueNames)
            {
                var peeker = new AzureServiceBusJobPeeker(options.ConnectionString);

                builder.UseJobPeeker(peeker);
            }

            return builder;
        }
    }
}
