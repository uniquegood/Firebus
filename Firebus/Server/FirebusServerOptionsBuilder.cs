using Firebus.Server.Filters;

namespace Firebus.Server
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

        public FirebusServerOptionsBuilder AddBeforeExecuteJobFilter<TFilter>() where TFilter : IBeforeExecuteJobFilter, new()
        {
            Options.BeforeExecuteJobFilters.Add(new TFilter());
            return this;
        }

        public FirebusServerOptionsBuilder AddAfterExecuteJobFilter(IAfterExecuteJobFilter filter)
        {
            Options.AfterExecuteJobFilters.Add(filter);
            return this;
        }

        public FirebusServerOptionsBuilder AddAfterExecuteJobFilter<TFilter>() where TFilter : IAfterExecuteJobFilter, new()
        {
            Options.AfterExecuteJobFilters.Add(new TFilter());
            return this;
        }

        public FirebusServerOptionsBuilder AddExecuteJobFilter(IExecuteJobFilter filter)
        {
            Options.BeforeExecuteJobFilters.Add(filter);
            Options.AfterExecuteJobFilters.Add(filter);
            return this;
        }

        public FirebusServerOptionsBuilder AddExecuteJobFilter<TFilter>() where TFilter : IExecuteJobFilter, new()
        {
            var filterInstance = new TFilter();
            Options.BeforeExecuteJobFilters.Add(filterInstance);
            Options.AfterExecuteJobFilters.Add(filterInstance);
            return this;
        }
    }
}
