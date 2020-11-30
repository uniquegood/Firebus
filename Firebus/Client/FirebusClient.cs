using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Firebus.Client.Filters;
using Firebus.Extensions;

namespace Firebus.Client
{
    public class FirebusClient
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFirebusJobRegister _jobRegister;
        private readonly ISet<IBeforeRegisterJobFilter> _beforeRegisterJobFilters;
        private readonly ISet<IAfterRegisterJobFilter> _afterRegisterJobFilters;

        public FirebusClient(IServiceProvider serviceProvider, FirebusClientOptions options)
        {
            _serviceProvider = serviceProvider;
            _jobRegister = options.JobRegister;

            _beforeRegisterJobFilters = options.BeforeRegisterJobFilters;
            _afterRegisterJobFilters = options.AfterRegisterJobFilters;
        }

        public Task RegisterJobAsync<TService>(Expression<Action<TService>> jobAction)
            => RegisterJobAsync(jobAction, null, null);

        public Task RegisterJobAsync<TService>(Expression<Action<TService>> jobAction, Dictionary<string, object> items)
            => RegisterJobAsync(jobAction, null, items);

        public Task RegisterJobAsync<TService>(Expression<Action<TService>> jobAction, DateTime? scheduledTimeUtc)
            => RegisterJobAsync(jobAction, scheduledTimeUtc, null);

        public async Task RegisterJobAsync<TService>(Expression<Action<TService>> jobAction, DateTime? scheduledTimeUtc,
            Dictionary<string, object> items)
        {
            var job = ExtractJob(jobAction);
            job.Items = items ?? new Dictionary<string, object>();

            var context = new JobContext(job, _serviceProvider);

            foreach (var filter in _beforeRegisterJobFilters)
            {
                if (!await filter.OnBeforeRegisterJob(context))
                    return;
            }

            await _jobRegister.RegisterJobAsync(job, scheduledTimeUtc);

            foreach(var filter in _afterRegisterJobFilters)
            {
                await filter.OnAfterRegisterJob(context);
            }
        }

        private FirebusJob ExtractJob<TService>(Expression<Action<TService>> jobAction)
        {
            var type = jobAction.Type.GenericTypeArguments[0].AssemblyQualifiedName;
            var methodCall = (MethodCallExpression) jobAction.Body;
            var args = methodCall.Arguments.Select(arg => arg.Evaluate()).ToArray();

            var job = new FirebusJob
            {
                ServiceTypeName = type,
                MethodName = methodCall.Method.Name,
                Parameters = args
            };

            return job;
        }
    }
}
