using System.Reflection;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QueuePackage;

public static class MassTransitConfiguration
{
    public static void MassTransitConfig(this WebApplicationBuilder builder, Assembly assembly)
    {
        builder.Services.AddScoped<MessagePublisher>();
        builder.Services.AddMassTransit(mt =>
        {
            mt.AddConsumers(assembly);
            mt.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ");

                var host = rabbitMqSettings.GetValue<string>("Host");
                var virtualHost = rabbitMqSettings.GetValue<string>("VirtualHostName");
                var username = rabbitMqSettings.GetValue<string>("Username");
                var password = rabbitMqSettings.GetValue<string>("Password");
                var useSsl = rabbitMqSettings.GetValue<bool>("UseSsl");
                var port = rabbitMqSettings.GetValue<ushort>("Port");

                cfg.Host(host, port,virtualHost, h =>
                {
                    h.Username(username);
                    h.Password(password);

                    if (useSsl)
                    {
                        h.UseSsl(s =>
                        {
                            s.Protocol = System.Security.Authentication.SslProtocols.Tls12;
                        });
                    }
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}