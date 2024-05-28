namespace UpscaleDown.EventDriven.Providers.Event.RabbitMQ;

public class RabbitMQOptions
{
    public string HOST { get; set; }

    internal int RetryCount = -1;
    internal int RetryDelay = -1;

    public void EnableRetryOnFailure(int retryCount, int retryDelay)
    {
        RetryCount = retryCount;
        RetryDelay = retryDelay;

        if (RetryDelay == -1 || RetryCount == -1)
        {
            throw new ArgumentException("You must specify retryCount and retryDelay", nameof(EnableRetryOnFailure));
        }
    }
}