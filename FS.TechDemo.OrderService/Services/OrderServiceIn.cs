using AutoMapper;
using FS.TechDemo.OrderService.Entities;
using FS.TechDemo.OrderService.Repositories;
using FS.TechDemo.Shared.communication.RabbitMQ.Contracts;
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

    public OrderService(ILogger<OrderService> logger, IMapper mapper, IOrderRepository orderRepository, IBus bus, IMessageScheduler messageScheduler)
    {
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

    public override async Task<Int32Value> CreateOrder(CreateOrderRequest request, ServerCallContext context)
    {
        var id = _orderRepository.AddOrder(request.Name, request.Number, request.Total);
        await _bus.Publish(new OrderDelivery { OrderName = request.Name }, context.CancellationToken);
        
        await ScheduleDeliveryMessage(id);

        return new Int32Value() {Value = id};
    }
    
    private async Task ScheduleDeliveryMessage(int id)
    {
        var messageScheduleTime = DateTime.UtcNow + TimeSpan.FromSeconds(60);
        
        await _messageScheduler.SchedulePublish<OrderDeliveryEventData>(messageScheduleTime,
            new
            {
                Id = id,
                DeliveryTime = DateTime.Now.AddMinutes(1.5)
            });
    }
}
