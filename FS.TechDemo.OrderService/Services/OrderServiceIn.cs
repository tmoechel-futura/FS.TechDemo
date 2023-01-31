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

    public OrderService(ILogger<OrderService> logger, IMapper mapper, IOrderRepository orderRepository, IBus bus) {  
        _logger = logger;
        _mapper = mapper;
        _orderRepository = orderRepository;
        _bus = bus;
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
        _logger.LogInformation("Publishing to Bus Name: {RequestName}, {RequestNumber}, {RequestTotal}", request.Name, request.Number, request.Total);
        await _bus.Publish(new OrderDelivery { OrderName = request.Name }, context.CancellationToken);
        return new Int32Value() {Value = id};
    }
}