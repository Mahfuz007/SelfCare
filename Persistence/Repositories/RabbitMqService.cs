using Application.Common.Interfaces;
using RabbitMQ.Client;

namespace Persistence.Repositories
{
    public class RabbitMqService : IRabbitMqService
    {
        private IConnection? _connection;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IEnumerable<string> _requiredQueues;

        public RabbitMqService(IEnumerable<string> RequiredQueues)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                RequestedConnectionTimeout = TimeSpan.FromSeconds(10)
            };
            _requiredQueues = RequiredQueues;
        }

        public void PublishToQueue(string queueName, object message)
        {
            throw new NotImplementedException();
        }


        public async Task InitializeAsync()
        {
            _connection = await _connectionFactory.CreateConnectionAsync();
            var channel = await _connection.CreateChannelAsync();

            foreach(var queue in _requiredQueues)
            {
                await channel.QueueDeclareAsync(queue: queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                Console.WriteLine($"Connected to rabbitMq queue. Queue Name: {queue}", queue);
            }

        }
    }
}
