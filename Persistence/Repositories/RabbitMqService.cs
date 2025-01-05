using Application.Common.Interfaces;
using Domain.Common;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Persistence.Repositories
{
    public class RabbitMqService : IRabbitMqService
    {
        private IChannel? _channel;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IEnumerable<string> _requiredQueues;

        public RabbitMqService(IEnumerable<string> RequiredQueues)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "0.0.0.0",
                UserName = "guest",
                Password = "guest",
                RequestedConnectionTimeout = TimeSpan.FromSeconds(10)
            };
            _requiredQueues = RequiredQueues;
        }

        public async void PublishToQueue(string queueName, object message)
        {
            if(_channel is null)
            {
                Console.WriteLine("Channel is not for rabbitMq connection");
                return;
            }
            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);
            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);
        }


        public async Task InitializeAsync()
        {
            var connection = await _connectionFactory.CreateConnectionAsync();
            if(connection is null)
            {
                Console.WriteLine("RabbitMq connection couldn't be connected!");
                return;
            }
            _channel = await connection.CreateChannelAsync();

            if(_channel is null)
            {
                Console.WriteLine("Channel is Null.RabbitMq connection couldn't be connected!");
                return;
            }

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (sender, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var commandWrapper = JsonConvert.DeserializeObject<QueueCommandWrapper>(message);

                if (commandWrapper == null)
                {
                    Console.WriteLine("Invalid message received");
                    return;
                }

                try
                {
                    var commandType = Type.GetType(commandWrapper.CommandName); // Ensure the type is resolvable
                    if (commandType == null)
                    {
                        throw new InvalidOperationException($"Unknown command type: {commandWrapper.CommandName}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            };

            foreach (var queue in _requiredQueues)
            {
                await _channel.QueueDeclareAsync(queue: queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                Console.WriteLine($"Connected to rabbitMq queue. Queue Name: {queue}", queue);
                await _channel.BasicConsumeAsync(queue: queue, autoAck: true, consumer: consumer);
            }

        }
    }
}
