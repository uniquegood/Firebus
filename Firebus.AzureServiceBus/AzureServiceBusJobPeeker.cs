using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebus.Manage;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace Firebus.AzureServiceBus
{
    public class AzureServiceBusJobPeeker : IFirebusJobPeeker
    {
        private static readonly Dictionary<string, MessageReceiver> _receivers = new Dictionary<string, MessageReceiver>();
        private static readonly Dictionary<string, QueueClient> _queueClients = new Dictionary<string, QueueClient>();

        private readonly string _connectionString;

        public AzureServiceBusJobPeeker(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<FirebusJob[]> GetAllRegisteredJobsAsync(string queueName)
        {
            var receiver = GetReceiver(queueName);

            const int maxCount = 100;
            long lastSeqNum = 0;

            List<Message> messages = new List<Message>();
            do
            {
                var newMessages = await receiver.PeekBySequenceNumberAsync(lastSeqNum, maxCount);
                messages.AddRange(newMessages);

                if (newMessages.Count < maxCount)
                    break;

                lastSeqNum = newMessages.Last().SystemProperties.SequenceNumber;
            } while (true);

            var jobs = messages
                .Select(msg =>
                {
                    var job = JsonConvert.DeserializeObject<FirebusJob>(Encoding.UTF8.GetString(msg.Body),
                        new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});

                    if (job.Items == null)
                    {
                        job.Items = new Dictionary<string, object>();
                    }

                    job.Items.Add("$sn", msg.SystemProperties.SequenceNumber);

                    return job;
                })
                .ToArray();

            return jobs;
        }

        public async Task CancelRegisteredJobAsync(string queueName, FirebusJob job)
        {
            var client = GetQueueClient(queueName);

            if (job.Items == null || !job.Items.ContainsKey("$sn"))
            {
                throw new InvalidOperationException("The job has insufficient information");
            }
            await client.CancelScheduledMessageAsync((long) job.Items["$sn"]);
        }

        private MessageReceiver GetReceiver(string queueName)
        {
            _receivers.TryGetValue(queueName, out var receiveClient);
            if (receiveClient == null)
            {
                receiveClient = new MessageReceiver(_connectionString, queueName);
                _receivers.Add(queueName, receiveClient);
            }

            return receiveClient;
        }

        private QueueClient GetQueueClient(string queueName)
        {
            _queueClients.TryGetValue(queueName, out var queueClient);
            if (queueClient == null)
            {
                queueClient = new QueueClient(_connectionString, queueName);
                _queueClients.Add(queueName, queueClient);
            }

            return queueClient;
        }
    }
}
