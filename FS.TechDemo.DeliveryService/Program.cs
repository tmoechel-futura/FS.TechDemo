using System.Reflection;
using FS.TechDemo.Shared.communication.database;
using FS.TechDemo.Shared.options;
using MassTransit;
using Quartz;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console()
//     .WriteTo.Seq("http://localhost:5341"));

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Assembly", typeof(Program).Assembly.GetName().Name!)
    .WriteTo.Console()
    .CreateLogger();

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
    
    x.AddSagaStateMachines(entryAssembly);
    x.AddSagas(entryAssembly);
    x.AddActivities(entryAssembly);
    
    x.AddPublishMessageScheduler();

    var configSection = builder.Configuration.GetSection(MessageBrokerOptions.MessageBroker);
    var messageBrokerOptions = new MessageBrokerOptions();
    configSection.Bind(messageBrokerOptions);
    
    Log.Logger.Information("Host DeliveryService: {RabbitMqHost}", messageBrokerOptions.Broker.RabbitMq.Host);
    
    x.UsingRabbitMq((context, rabbitMqCfg) => {
        rabbitMqCfg.Host(messageBrokerOptions.Broker.RabbitMq.Host,
            messageBrokerOptions.Broker.RabbitMq.VirtualHost,
            h => {
                h.Username(messageBrokerOptions.Broker.RabbitMq.Username);
                h.Password(messageBrokerOptions.Broker.RabbitMq.Password);
            });
        rabbitMqCfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.Run();