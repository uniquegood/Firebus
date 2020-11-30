using System;
using System.Collections.Generic;
using System.Text;
using Firebus.Server;

namespace Firebus.AzureServiceBus
{
    public static class FirebusServerAzureServiceBusExtensions
    {
        public static FirebusServerOptionsBuilder UseAzureServiceBus(this FirebusServerOptionsBuilder builder,
            AzureServiceBusServerOptions options)
        {
            foreach (var queue in options.QueueNames)
            {
                var receiver = new AzureServiceBusJobReceiver(options.ConnectionString, queue);

                builder.AddJobReceiver(receiver);
            }

            return builder;
        }
    }
}
