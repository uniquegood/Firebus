using System.Collections.Generic;
using Firebus.Server.Filters;

namespace Firebus.Server
{
    public class FirebusServerOptions
    {
        public ISet<IFirebusJobReceiver> JobReceivers { get; } = new HashSet<IFirebusJobReceiver>();

        internal ISet<IBeforeExecuteJobFilter> BeforeExecuteJobFilters { get; set; } = new HashSet<IBeforeExecuteJobFilter>();
        internal ISet<IAfterExecuteJobFilter> AfterExecuteJobFilters { get; set; } = new HashSet<IAfterExecuteJobFilter>();

        internal IFirebusExceptionHandler ExceptionHandler { get; set; }
    }
}
