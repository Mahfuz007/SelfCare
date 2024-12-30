namespace Domain.Common
{
    public class QueueCommandWrapper
    {
        public string Body { get; set; } = string.Empty ;
        public string CommandName { get; set; } = string.Empty ;
        public string QueueName {  get; set; } = string.Empty ;
    }
}
