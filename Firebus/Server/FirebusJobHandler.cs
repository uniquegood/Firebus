using System;
using System.Linq;
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

            try
            {
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
            catch (Exception e)
            {
                if (_serverOptions.ExceptionHandler != null)
                {
                    await _serverOptions.ExceptionHandler.HandleAsync(e);
                }
            }
        }

        private async Task ExecuteJobAsync(FirebusJob job, JobContext context)
        {
            var type = Type.GetType(job.ServiceTypeName);
            if (type == null)
                throw new TypeLoadException($"Failed to load type '{job.ServiceTypeName}'");

            var args = job.ParameterTypeNames
                .Select(tn => Type.GetType(tn))
                .Zip(job.Parameters, (t, arg) => (Type: t, Argument: arg))
                .Select(pair =>
                    pair.Type.IsEnum
                        ? Convert.ChangeType(pair.Argument, pair.Type.GetEnumUnderlyingType())
                        : pair.Argument)
                .ToArray();

            var method = type.GetMethods()
                .SingleOrDefault(m =>
                    m.Name == job.MethodName
                    && m.GetParameters()
                        .Zip(job.ParameterTypeNames,
                            (p1, p2) => (p1.ParameterType, ArgumentType: Type.GetType(p2)))
                        .All(pair => pair.ParameterType.IsAssignableFrom(pair.ArgumentType)));

            if (method == null)
                throw new MissingMethodException($"Failed to find matched method '{job.MethodName}'");

            var instance = context.ServiceProvider.GetService(type);
            if (instance == null)
                throw new Exception($"Could not resolve a service of type '{type.FullName}'");

            if (method.Invoke(instance, args) is Task returnTask)
                await returnTask;
        }
    }
}
