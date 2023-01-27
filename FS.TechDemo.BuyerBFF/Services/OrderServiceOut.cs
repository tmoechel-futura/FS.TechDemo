using FS.TechDemo.BuyerBFF.Configuration;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Shared;

namespace FS.TechDemo.BuyerBFF.Services;

internal class OrderServiceOut : IOrderServiceOut
{
    private readonly IConfiguration _configuration;
    private readonly IOptions<GrpcOptions> _grpcOptions;
    private readonly ILogger<OrderServiceOut> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public OrderServiceOut(IConfiguration configuration, IOptions<GrpcOptions> grpcOptions, ILogger<OrderServiceOut> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _configuration = configuration;
        _grpcOptions = grpcOptions;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    [Authorize]
    public async Task<List<OrderResponse>> GetOrderListAsync(CancellationToken cancellationToken)
    {
        
        var address = _grpcOptions.Value.Grpc.FirstOrDefault(t => t.Destination == "OrderService")
            ?.Channel
            .Endpoint;
        _logger.LogInformation("address gprc destination: {Address}", address);
        if (address != null)
        {
            var channel = GrpcChannel.ForAddress(address);
            _logger.LogInformation("address gprc destination: {Address}", address);
        
            var client = new GrpcOrderService.GrpcOrderServiceClient(channel);
            _logger.LogInformation("channel : {ChannelTarget}", channel.Target);
            _logger.LogInformation("channel : {ChannelState}", channel.State);
        
           using (var scope = _serviceScopeFactory.CreateScope())
           {
               var scopedServices = scope.ServiceProvider;
               var loggerFactory = scopedServices.GetRequiredService<ILoggerFactory>();
               var logger = loggerFactory.CreateLogger<OrderResponse>();
               logger.LogInformation("calling order service endpoint on {Address}", address);
           }

           var result = new List<OrderResponse>(); 
           try
           {
               var orderListResponse = client.GetOrders(new Empty());  
               var responseStream = orderListResponse.ResponseStream;

               
               while (await responseStream.MoveNext(cancellationToken)) {
                   var responseItem = responseStream.Current;
                   result.Add(responseItem);
               }
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               _logger.LogInformation("channel : {ChannelState}", e.Message + "InnerException: " + e.InnerException?.Message);
               throw;
           } 
           
            return result;
        }
        return new List<OrderResponse>();
    }

    public async Task<Int32Value> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var address = _grpcOptions.Value.Grpc.FirstOrDefault(t => t.Destination == "OrderService")
            ?.Channel
            .Endpoint;
        
        if (address != null)
        {
            var channel = GrpcChannel.ForAddress(address);
            var client = new GrpcOrderService.GrpcOrderServiceClient(channel);
    
            var orderCreateResponse = await client.CreateOrderAsync(request);
            return orderCreateResponse;
        }
    
        return new Int32Value();
    }
}
