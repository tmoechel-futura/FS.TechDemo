using FS.TechDemo.BuyerBFF.GraphQL.Extensions;
using FS.TechDemo.BuyerBFF.GraphQL.RequestHandler;
using FS.TechDemo.BuyerBFF.GraphQL.Types;
using FS.TechDemo.BuyerBFF.GraphQL.Types.Order;
using MediatR;

namespace FS.TechDemo.BuyerBFF.GraphQL;

public class BuyerQuery  : ObjectType
{
    private readonly IMediator _mediator;
    private readonly ILoggerFactory _loggerFactory;
    
    public BuyerQuery(IMediator mediator, ILoggerFactory loggerFactory)
    {
        _mediator = mediator;
        _loggerFactory = loggerFactory;
    }
    
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        base.Configure(descriptor);
        descriptor.Field("OrderList").Type<ListType<OrderType>>()
            .Resolve(_mediator.GetResolverFunc<OrderTypeResolvableRequest>(_loggerFactory));
    }
}