using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Firebus.Server;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Firebus.AzureServiceBus
{
    public class AzureServiceBusJobReceiver: IFirebusJobReceiver
    {
        private readonly QueueClient _client;
        private FirebusJobHandler _jobHandler;

        public AzureServiceBusJobReceiver(string connectionString, string queueName = "default")
        {
            _client = new QueueClient(connectionString, queueName);
        }

        public void BeginReceive()
        {
            _client.RegisterMessageHandler(HandleMessageAsync, ExceptionReceivedAsync);
        }

        private Task ExceptionReceivedAsync(ExceptionReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            if (_jobHandler == null)
                throw new InvalidOperationException("JobHandler should be not null");

            var job = JsonConvert.DeserializeObject<FirebusJob>(Encoding.UTF8.GetString(message.Body),
                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});

            _jobHandler.HandleJobAsync(job);
        }

        public void RegisterJobHandler(FirebusJobHandler handler)
        {
            _jobHandler = handler;
        }
    }
}
