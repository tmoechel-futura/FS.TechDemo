using Shared;

namespace FS.TechDemo.BuyerBFF.Services;

public interface IOrderServiceOut
{
    Task<List<OrderResponse>> GetOrderListAsync(CancellationToken cancellationToken);
    Task<string> CreateOrder(OrderCreateRequest request, CancellationToken cancellationToken);
}
