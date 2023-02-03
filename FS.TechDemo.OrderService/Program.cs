using System.Reflection;
using FS.TechDemo.OrderService.Repositories;
using FS.TechDemo.OrderService.Services;
using FS.TechDemo.Shared;
using FS.TechDemo.Shared.options;
using MassTransit;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));

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

    Log.Logger.Information("Host OrderService: {RabbitMqHost}", messageBrokerOptions.Broker.RabbitMq.Host);
    x.UsingRabbitMq((context, rabbitMqCfg) =>
    {
        rabbitMqCfg.Host(messageBrokerOptions.Broker.RabbitMq.Host,
            messageBrokerOptions.Broker.RabbitMq.VirtualHost,
            h =>
            {
                h.Username(messageBrokerOptions.Broker.RabbitMq.Username);
                h.Password(messageBrokerOptions.Broker.RabbitMq.Password);
            });
        rabbitMqCfg.ConfigureEndpoints(context);
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


// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();
app.UseCustomRequestLogging();
app.MapGrpcService<OrderService>();

app.Run();