using AutoMapper;
using FS.TechDemo.OrderService.Entities;
using FS.TechDemo.OrderService.Repositories;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Shared;

namespace FS.TechDemo.OrderService.Services;

public class OrderService : GrpcOrderService.GrpcOrderServiceBase
{
    private readonly ILogger<OrderService> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public OrderService(ILogger<OrderService> logger, IMapper mapper, IOrderRepository orderRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _orderRepository = orderRepository;
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
}