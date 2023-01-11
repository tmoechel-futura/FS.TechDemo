using System.Reflection;
using FS.TechDemo.Shared.communication.RabbitMQ.Extensions;
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

builder.AddRabbitMQConfiguration();

var app = builder.Build();

app.Run();