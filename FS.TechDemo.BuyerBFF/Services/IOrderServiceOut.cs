using Google.Protobuf.WellKnownTypes;
using Shared;

namespace FS.TechDemo.BuyerBFF.Services;

public interface IOrderServiceOut
{
    Task<List<OrderResponse>> GetOrderListAsync(CancellationToken cancellationToken);
    Task<Int32Value> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken);
}
