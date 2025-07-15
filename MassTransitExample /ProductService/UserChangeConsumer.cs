using MassTransit;
using QueuePackage;

namespace ProductService;

public class UserChangeConsumer()
    : IConsumer<UserChangeMessage>
{
    public Task Consume(ConsumeContext<UserChangeMessage> context)
    {
        Console.WriteLine(
            $"UserChangeConsumer Consume method called. UserId: {context.Message.Id} Name: {context.Message.Name}");
        return Task.CompletedTask;
    }
}