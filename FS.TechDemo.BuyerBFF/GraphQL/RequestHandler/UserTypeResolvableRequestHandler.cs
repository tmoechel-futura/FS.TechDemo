using FS.TechDemo.BuyerBFF.Services;
using MediatR;

namespace FS.TechDemo.BuyerBFF.GraphQL.RequestHandler;

public class UserTypeResolvableRequest : ResolvableRequest
{ }

public class UserTypeResolvableRequestHandler : IRequestHandler<UserTypeResolvableRequest, object>
{
    private IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<UserTypeResolvableRequestHandler> _logger;

    public UserTypeResolvableRequestHandler(IServiceScopeFactory serviceScopeFactory, ILogger<UserTypeResolvableRequestHandler> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task<object> Handle(UserTypeResolvableRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting orders from order service");
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var _userServiceOut = scopedServices.GetRequiredService<IUserServiceOut>();
            var orderList = await _userServiceOut.GetUserListAsync(cancellationToken);
            return orderList;
        }
    }
}
