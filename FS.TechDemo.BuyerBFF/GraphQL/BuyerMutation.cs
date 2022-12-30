using FS.TechDemo.BuyerBFF.GraphQL.Extensions;
using FS.TechDemo.BuyerBFF.GraphQL.RequestHandler;
using FS.TechDemo.BuyerBFF.GraphQL.Types.Order;
using MediatR;

namespace FS.TechDemo.BuyerBFF.GraphQL;

public class BuyerMutation: ObjectType
{
    private readonly IMediator _mediator;
    private readonly ILoggerFactory _loggerFactory;

    public BuyerMutation(IMediator mediator, ILoggerFactory loggerFactory)
    {
        _mediator = mediator;
        _loggerFactory = loggerFactory;
    }
    
    
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        base.Configure(descriptor);
        descriptor.Field("CreateOrder").Type<StringType>()
            .Argument("CreateOrderInput", argument => argument.Type<NonNullType<OrderInputType>>())
            .Resolve(_mediator.GetResolverFunc<CreateOrderRequest>(_loggerFactory));
    }
}