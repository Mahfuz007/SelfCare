namespace Application.Common.Interfaces
{
    public interface IServiceClient
    {
        Response SendToQueue<Response>(string queueName, Object payload) where Response : new();
    }
}
