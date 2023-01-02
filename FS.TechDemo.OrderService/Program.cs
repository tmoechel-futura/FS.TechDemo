using System.Reflection;
using FS.TechDemo.OrderService.Repositories;
using FS.TechDemo.OrderService.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLogging();

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();


// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();
app.MapGrpcService<OrderService>();

app.Run();