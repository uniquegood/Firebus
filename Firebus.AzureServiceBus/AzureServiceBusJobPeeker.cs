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
        private readonly string _connectionString;

        public AzureServiceBusJobPeeker(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<FirebusJob[]> GetAllRegisteredJobsAsync(string queueName)
        {
            var receiver = GetReceiver(queueName);

            const int maxCount = 100;
            var lastSeqNum = 0;

            List<Message> messages = new List<Message>();
            do
            {
                var newMessages = await receiver.PeekBySequenceNumberAsync(lastSeqNum, maxCount);
                messages.AddRange(newMessages);
                if (newMessages.Count < maxCount)
                    break;
            } while (true);

            return messages
                .Select(msg =>
                    JsonConvert.DeserializeObject<FirebusJob>(Encoding.UTF8.GetString(msg.Body),
                        new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All}))
                .ToArray();
        }

        private MessageReceiver GetReceiver(string queueName)
        {
            _receivers.TryGetValue(queueName, out var queueClient);
            if (queueClient == null)
            {
                queueClient = new MessageReceiver(_connectionString, queueName);
                _receivers.Add(queueName, queueClient);
            }

            return queueClient;
        }
    }
}
