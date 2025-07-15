using QueuePackage;

namespace AuthService;

public class UserChangeBackgroundService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var publisher = scope.ServiceProvider.GetRequiredService<MessagePublisher>();

            await publisher.PublishAsync(new UserChangeMessage()
            {
                Id = Guid.NewGuid(),
                Name = "Test User " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }, stoppingToken);
            await Task.Delay(1000, stoppingToken); // 1 saniye bekleme
        }
    }
}