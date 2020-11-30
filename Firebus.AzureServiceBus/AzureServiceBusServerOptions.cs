using System;
using System.Collections.Generic;
using System.Text;

namespace Firebus.AzureServiceBus
{
    public class AzureServiceBusServerOptions
    {
        public string ConnectionString { get; set; }
        public string[] QueueNames { get; set; }
    }
}
