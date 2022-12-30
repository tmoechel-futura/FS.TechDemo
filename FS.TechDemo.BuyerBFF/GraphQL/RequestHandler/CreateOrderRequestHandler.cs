using AutoMapper;
using FS.TechDemo.BuyerBFF.Services;
using MediatR;
using Shared;

namespace FS.TechDemo.BuyerBFF.GraphQL.RequestHandler;


public class CreateOrderRequest : ResolvableRequest
{
    
}


public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, object>
{
    private readonly IMapper _mapper;
    private readonly IOrderServiceOut _orderServiceOut;

    public CreateOrderRequestHandler(IMapper mapper,  IOrderServiceOut orderServiceOut)
    {
        _mapper = mapper;
        _orderServiceOut = orderServiceOut;
    }


    public async Task<object> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var createOrderId = await _orderServiceOut.GetOrderListAsync(cancellationToken);
        return createOrderId;
    }
}