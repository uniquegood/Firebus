using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebus.Client;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Firebus.AzureServiceBus
{
    public class AzureServiceBusJobRegister : IFirebusJobRegister
    {
        private static readonly Dictionary<string, QueueClient> _queueClients = new Dictionary<string, QueueClient>();

        private readonly string _connectionString;
        private readonly string _defaultQueueName;

        public AzureServiceBusJobRegister(string connectionString, string defaultQueueName)
        {
            _connectionString = connectionString;
            _defaultQueueName = defaultQueueName ?? "default";
        }

        public async Task RegisterJobAsync(FirebusJob job, DateTime? scheduledTimeUtc)
        {
            object queueNameObj = null;
            job.Items?.TryGetValue("$queue", out queueNameObj);

            var queueName = (queueNameObj as string) ?? _defaultQueueName;

            _queueClients.TryGetValue(queueName, out var queueClient);
            if (queueClient == null)
            {
                queueClient = new QueueClient(_connectionString, queueName);
                _queueClients.Add(queueName, queueClient);
            }

            await queueClient.SendAsync(new Message
            {
                ContentType = "application/json",
                Body = Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(job,
                        new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All})),
                ScheduledEnqueueTimeUtc = scheduledTimeUtc ?? default
            });
        }
    }
}
