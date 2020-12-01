using System;
using System.Collections.Generic;
using System.Text;
using Firebus.Client;
using Firebus.Server;

namespace Firebus.AzureServiceBus
{
    public static class FirebusClientAzureServiceBusExtensions
    {
        public static FirebusClientOptionsBuilder UseAzureServiceBus(this FirebusClientOptionsBuilder builder, AzureServiceBusClientOptions options)
        {
            var register = new AzureServiceBusJobRegister(options.ConnectionString, options.DefaultQueueName);
            builder.UseJobRegister(register);

            return builder;
        }
    }
}
