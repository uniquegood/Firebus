using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Firebus
{
    public class FirebusServerOptions
    {
        public ISet<IFirebusJobReceiver> JobReceivers { get; internal set; } = new HashSet<IFirebusJobReceiver>();

        internal ISet<IBeforeExecuteJobFilter> BeforeExecuteJobFilters { get; set; } = new HashSet<IBeforeExecuteJobFilter>();
        internal ISet<IAfterExecuteJobFilter> AfterExecuteJobFilters { get; set; } = new HashSet<IAfterExecuteJobFilter>();
    }
}
