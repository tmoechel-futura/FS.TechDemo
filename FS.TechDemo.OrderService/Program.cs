using System.Reflection;
using FS.TechDemo.OrderService.Repositories;
using FS.TechDemo.OrderService.Services;
using FS.TechDemo.Shared;
using FS.TechDemo.Shared.communication.RabbitMQ.Extensions;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

// builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console()
//     .WriteTo.Seq("http://localhost:5341"));
//
// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
//     .Enrich.FromLogContext()
//     .Enrich.WithMachineName()
//     .Enrich.WithProperty("Assembly", typeof(Program).Assembly.GetName().Name!)
//     .WriteTo.Console()
//     .CreateLogger();

//builder.AddRabbitMQConfiguration();

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


var app = builder.Build();
//app.UseCustomRequestLogging();
app.MapGrpcService<OrderService>();

app.Run();