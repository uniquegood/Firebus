using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Firebus
{
    public class FirebusServerOptionsBuilder
    {
        public FirebusServerOptions Options { get; } = new FirebusServerOptions();

        public void AddJobReceiver(IFirebusJobReceiver receiver)
        {
            Options.JobReceivers.Add(receiver);
        }

        public FirebusServerOptionsBuilder AddBeforeExecuteJobFilter(IBeforeExecuteJobFilter filter)
        {
            Options.BeforeExecuteJobFilters.Add(filter);
            return this;
        }

        public FirebusServerOptionsBuilder AddAfterExecuteJobFilter(IAfterExecuteJobFilter filter)
        {
            Options.AfterExecuteJobFilters.Add(filter);
            return this;
        }

        public FirebusServerOptionsBuilder AddExecuteJobFilter(IExecuteJobFilter filter)
        {
            Options.BeforeExecuteJobFilters.Add(filter);
            Options.AfterExecuteJobFilters.Add(filter);
            return this;
        }
    }
}
