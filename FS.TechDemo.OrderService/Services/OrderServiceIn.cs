using AutoMapper;
using FS.TechDemo.OrderService.Entities;
using FS.TechDemo.OrderService.Repositories;
using FS.TechDemo.Shared.communication.RabbitMQ.Contracts;
using FS.TechDemo.Shared.communication.RabbitMQ.Schedules;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MassTransit;
using Shared;

namespace FS.TechDemo.OrderService.Services;

public class OrderService : GrpcOrderService.GrpcOrderServiceBase
{
    private readonly ILogger<OrderService> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IBus _bus;
    private readonly IMessageScheduler _messageScheduler;

    public OrderService(ILogger<OrderService> logger, IMapper mapper, IOrderRepository orderRepository, IBus bus,  IMessageScheduler messageScheduler) {  
        _logger = logger;
        _mapper = mapper;
        _orderRepository = orderRepository;
        _bus = bus;
        _messageScheduler = messageScheduler;
    }

    public override async Task GetOrders(Empty request, IServerStreamWriter<OrderResponse> responseStream, ServerCallContext context)
    {
        var entityOrderList = _orderRepository.GetOrderList();
        foreach (var order in entityOrderList)
        {
            if (context.CancellationToken.IsCancellationRequested) break;

            var recipeResponse = _mapper.Map<Order, OrderResponse>(order);
            await responseStream.WriteAsync(recipeResponse);
        }
    }

    // demo comment creates an order to test github actions changed comment
    public override async Task<Int32Value> CreateOrder(CreateOrderRequest request, ServerCallContext context)
    {
        var id = _orderRepository.AddOrder(request.Name, request.Number, request.Total);
        _logger.LogInformation("Publishing to Bus Name: {RequestName}, {RequestNumber}, {RequestTotal}", request.Name, request.Number, request.Total);
        await _bus.Publish(new OrderDelivery { OrderName = request.Name }, context.CancellationToken);
        
        _logger.LogInformation("Bus URI: {BusUri}", $"rabbitmq://{_bus.Address.Host}/quartz");
        
        await _messageScheduler.SchedulePublish(TimeSpan.FromSeconds(30), new DemoMessage { Value = "Hello, World" }, context.CancellationToken);
        
        return new Int32Value() {Value = id};
    }
}