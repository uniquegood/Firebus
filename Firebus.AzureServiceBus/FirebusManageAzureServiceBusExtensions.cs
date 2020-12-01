using System;
using System.Collections.Generic;
using System.Text;
using Firebus.Manage;

namespace Firebus.AzureServiceBus
{
    public static class FirebusManageAzureServiceBusExtensions
    {
        public static FirebusManageOptionsBuilder UseAzureServiceBus(this FirebusManageOptionsBuilder builder,
            string connectionString)
        {
            var peeker = new AzureServiceBusJobPeeker(connectionString);

            builder.UseJobPeeker(peeker);

            return builder;
        }
    }
}
