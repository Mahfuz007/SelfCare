namespace Application.Common.Interfaces
{
    public interface IRabbitMqService
    {
        void PublishToQueue(string queueName, Object message);
        Task InitializeAsync();
    }
}
