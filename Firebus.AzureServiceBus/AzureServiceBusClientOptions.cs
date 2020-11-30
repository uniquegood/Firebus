using System;
using System.Collections.Generic;
using System.Text;

namespace Firebus.AzureServiceBus
{
    public class AzureServiceBusClientOptions
    {
        public string ConnectionString { get; set; }
        public string DefaultQueueName { get; set; }
    }
}
