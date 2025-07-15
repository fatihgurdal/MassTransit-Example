using MassTransit;

namespace QueuePackage;

public class MessagePublisher(IPublishEndpoint publishEndpoint)
{
    public async Task PublishAsync<T>(T message, CancellationToken stoppingToken = default)
    {
        await publishEndpoint.Publish(message, context =>
        {
            // context.CorrelationId= Guid.NewGuid(); // Varsa context'e farklı verilerde taşıyabilirsiniz
        }, stoppingToken);
    }
}