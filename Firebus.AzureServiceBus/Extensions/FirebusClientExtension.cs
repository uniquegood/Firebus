using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Firebus.Client;

namespace Firebus.AzureServiceBus.Extensions
{
    public static class FirebusClientExtension
    {
        public static Task RegisterJobInQueueAsync<TService>(this FirebusClient client,
            Expression<Action<TService>> jobAction, string queue)
            => client.RegisterJobAsync(jobAction, null,
                new Dictionary<string, object> {["$queue"] = queue});

        public static Task RegisterJobInQueueAsync<TService>(this FirebusClient client,
            Expression<Action<TService>> jobAction, DateTime? scheduledTimeUtc, string queue)
            => client.RegisterJobAsync(jobAction, scheduledTimeUtc,
                new Dictionary<string, object> {["$queue"] = queue});
    }
}
