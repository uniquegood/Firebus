using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Firebus.Server
{
    public class FirebusJobHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly FirebusServerOptions _serverOptions;

        public FirebusJobHandler(IServiceProvider serviceProvider, FirebusServerOptions serverOptions)
        {
            _serviceProvider = serviceProvider;
            _serverOptions = serverOptions;
        }

        public async Task HandleJobAsync(FirebusJob job)
        {
            var scope = _serviceProvider.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var contextAccessor = serviceProvider.GetService<JobExecutionContextAccessor>();

            var context = new JobContext(job, serviceProvider);
            contextAccessor.Context = context;

            foreach (var filter in _serverOptions.BeforeExecuteJobFilters)
            {
                if (!await filter.OnBeforeExecuteJob(context))
                    return;
            }

            await ExecuteJobAsync(job, context);

            foreach (var filter in _serverOptions.AfterExecuteJobFilters)
            {
                await filter.OnAfterExecuteJob(context);
            }
        }

        private async Task ExecuteJobAsync(FirebusJob job, JobContext context)
        {
            var type = Type.GetType(job.ServiceTypeName);
            if (type == null)
                throw new TypeLoadException($"Failed to load type '{job.ServiceTypeName}'");

            var method = type.GetMethod(job.MethodName);
            if (method == null)
                throw new MissingMethodException($"Failed to find method '{job.MethodName}'");

            var instance = context.ServiceProvider.GetService(type);
            if (instance == null)
                throw new Exception($"Could not resolve a service of type '{type.FullName}'");

            if (method.Invoke(instance, job.Parameters) is Task returnTask)
                await returnTask;
        }
    }
}
