using System.Reflection;
using FS.TechDemo.BuyerBFF.Configuration;
using FS.TechDemo.BuyerBFF.GraphQL;
using FS.TechDemo.BuyerBFF.Services;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

// graphql specific
builder.Services
    .AddGraphQLServer()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddQueryType<BuyerQuery>()
    .AddMutationType<BuyerMutation>();

// interface registration
builder.Services.AddScoped<IOrderServiceOut, OrderServiceOut>();
builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddOptions().Configure<GrpcOptions>(builder.Configuration.GetSection(GrpcOptions.GrpcOut));

var app = builder.Build();
app.UseRouting();
app.MapGraphQL();

app.Run();