using System;
using System.Collections.Generic;

namespace Firebus
{
    public class JobContext
    {
        public IServiceProvider ServiceProvider { get; }
        public FirebusJob Job { get; }

        public JobContext(FirebusJob job, IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Job = job;
        }
    }
}
