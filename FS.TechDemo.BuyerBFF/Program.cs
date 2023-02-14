using System.Reflection;
using FS.TechDemo.BuyerBFF.Configuration;
using FS.TechDemo.BuyerBFF.GraphQL;
using FS.TechDemo.BuyerBFF.IdentityProvider.Extensions;
using FS.TechDemo.BuyerBFF.Services;
using FS.TechDemo.Shared;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
//     .Enrich.FromLogContext()
//     .Enrich.WithMachineName()
//     .Enrich.WithProperty("Assembly", typeof(Program).Assembly.GetName().Name!)
//     .WriteTo.Console()
//     .CreateLogger();

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
builder.Services.AddKeycloak(builder.Configuration);

var app = builder.Build();
app.UseRouting();
//app.UseCustomRequestLogging();
app.MapGraphQL();

app.Run();