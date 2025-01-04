using Application.Common.Interfaces;
using Newtonsoft.Json;

namespace Persistence.Repositories
{
    public class ServiceClient : IServiceClient
    {
        private readonly IRabbitMqService _rabbitMqService;

        public ServiceClient(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        public Response SendToQueue<Response>(string queueName, object payload) where Response : new()
        {
            var rabitMqMessage = new
            {
                Body = JsonConvert.SerializeObject(payload),
                CommandName = payload.GetType().Name,
                QueueName = queueName
            };

            _rabbitMqService.PublishToQueue(queueName, rabitMqMessage);

            return new Response();
        }
    }
}
