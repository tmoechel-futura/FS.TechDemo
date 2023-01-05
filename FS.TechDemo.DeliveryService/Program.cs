using System.Reflection;
using MassTransit;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

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

    x.UsingRabbitMq((context, cfg) =>
    {
        //context is the registration context, used to configure endpoints. cfg is the bus factory configurator
        cfg.Host("localhost", "/", h =>
        {
            h.Username("rabbitmq-user");
            h.Password("rabbitmq-password");
        });
        cfg.UseMessageRetry(r=>
        {
            r.Immediate(5);

        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.Run();