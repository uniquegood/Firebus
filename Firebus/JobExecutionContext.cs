using System;
using System.Collections.Generic;
using System.Text;

namespace Firebus
{
    public class JobExecutionContext
    {
        public IServiceProvider ServiceProvider { get; }
        public Dictionary<string, object> Items { get; }

        public JobExecutionContext(IServiceProvider serviceProvider, Dictionary<string, object> items)
        {
            ServiceProvider = serviceProvider;
            Items = items;
        }
    }
}
