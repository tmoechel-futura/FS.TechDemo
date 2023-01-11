using System.Reflection;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FS.TechDemo.Shared.communication.RabbitMQ.Extensions;

public static class RabbitMQConfigurationExtension
{
    public static void AddRabbitMQConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x =>
        {
            //Kebab Case	submit-order
            x.SetKebabCaseEndpointNameFormatter();

            // By default, sagas are in-memory, but should be changed to a durable
            // saga repository.
            x.SetInMemorySagaRepositoryProvider();

            var entryAssembly = Assembly.GetEntryAssembly();

            // adds all consumers with IConsumer interface within assembly
            x.AddConsumers(entryAssembly);
    
            // x.AddSagaStateMachines(entryAssembly);
            // x.AddSagas(entryAssembly);
            // x.AddActivities(entryAssembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                //context is the registration context, used to configure endpoints. cfg is the bus factory configurator
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("rabbitmq-user");
                    h.Password("rabbitmq-password");
                });
                cfg.ConfigureEndpoints(context);
               
                // cfg.UseDelayedRedelivery(r => {
                //     var firstDelayedRedeliveryIntervalMinutes = TimeSpan.FromMinutes(1);
                //     //var secondDelayedRedeliveryIntervalMinutes = TimeSpan.FromMinutes(1);
                //     r.Handle<ConsumerException>();
                //     // var thirdDelayedRedeliveryIntervalMinutes = TimeSpan.FromMinutes(4);
                //     r.Intervals(firstDelayedRedeliveryIntervalMinutes);
                // });
                
                cfg.UseMessageRetry(r =>
                {
                    var messageRetryInitialIntervalMilliseconds = TimeSpan.FromMilliseconds(1000);
                    var messageRetryIntervalIncrementMilliseconds = TimeSpan.FromMilliseconds(500);
                    r.Handle<ConsumerException>();
                    // messages are retried before they go to the error queue
                    r.Incremental(5, messageRetryInitialIntervalMilliseconds, messageRetryIntervalIncrementMilliseconds);
                });
                cfg.UseInMemoryOutbox();
            });
        });

        builder.Services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                // if specified, waits until the bus is started before
                // returning from IHostedService.StartAsync
                // default is false
                options.WaitUntilStarted = true;

                // if specified, limits the wait time when starting the bus
                options.StartTimeout = TimeSpan.FromSeconds(10);

                // if specified, limits the wait time when stopping the bus
                options.StopTimeout = TimeSpan.FromSeconds(30);
            });
    }
    


}