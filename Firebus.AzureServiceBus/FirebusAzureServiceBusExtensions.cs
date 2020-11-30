using System;
using System.Collections.Generic;
using System.Text;

namespace Firebus.AzureServiceBus
{
    public static class FirebusAzureServiceBusExtensions
    {
        public static FirebusServerOptionsBuilder UseAzureServiceBus(this FirebusServerOptionsBuilder builder,
            AzureServiceBusOptions options)
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
