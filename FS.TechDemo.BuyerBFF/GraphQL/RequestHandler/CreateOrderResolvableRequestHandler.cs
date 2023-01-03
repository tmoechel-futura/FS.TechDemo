using AutoMapper;
using FS.TechDemo.BuyerBFF.Services;
using MediatR;
using Shared;

namespace FS.TechDemo.BuyerBFF.GraphQL.RequestHandler;


public class CreateOrderResolvableRequest : ResolvableRequest
{
    
}


public class CreateOrderResolvableRequestHandler : IRequestHandler<CreateOrderResolvableRequest, object>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMapper _mapper;

    public CreateOrderResolvableRequestHandler(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _mapper = mapper;
    }


    public async Task<object> Handle(CreateOrderResolvableRequest resolvableRequest, CancellationToken cancellationToken)
    {
        var resolveFieldContext = resolvableRequest.GetResolveFieldContext();
        var createOrderRequest = resolveFieldContext.ArgumentValue<CreateOrderRequest>("CreateOrderInput");

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var _orderServiceOut = scopedServices.GetRequiredService<IOrderServiceOut>();
        
            var createOrderId = await _orderServiceOut.CreateOrder(createOrderRequest, cancellationToken);
            return createOrderId.Value;
        }
    }
}