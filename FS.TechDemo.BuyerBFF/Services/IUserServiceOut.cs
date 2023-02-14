using Google.Protobuf.WellKnownTypes;
using Shared;

namespace FS.TechDemo.BuyerBFF.Services;

public interface IUserServiceOut
{
    Task<List<OrderResponse>> GetUserListAsync(CancellationToken cancellationToken);
    Task<Int32Value> CreateUser(CreateOrderRequest request, CancellationToken cancellationToken);
}
