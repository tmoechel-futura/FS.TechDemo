using FS.TechDemo.BuyerBFF.Services;
using MediatR;

namespace FS.TechDemo.BuyerBFF.GraphQL.RequestHandler;

public class OrderTypeResolvableRequest : ResolvableRequest
{ }

public class OrderTypeResolvableRequestHandler : IRequestHandler<OrderTypeResolvableRequest, object>
{
    private IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<OrderTypeResolvableRequestHandler> _logger;

    public OrderTypeResolvableRequestHandler(IServiceScopeFactory serviceScopeFactory, ILogger<OrderTypeResolvableRequestHandler> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task<object> Handle(OrderTypeResolvableRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting orders from order service");
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var _orderServiceOut = scopedServices.GetRequiredService<IOrderServiceOut>();
            var orderList = await _orderServiceOut.GetOrderListAsync(cancellationToken);
            return orderList;
        }
    }
}
