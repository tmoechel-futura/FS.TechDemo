using Shared;

namespace FS.TechDemo.BuyerBFF.GraphQL.Types.Order;

public class OrderTypeInput : InputObjectType<CreateOrderRequest>
{
    protected override void Configure(IInputObjectTypeDescriptor<CreateOrderRequest> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(context => context.Number).Type<StringType>();
        descriptor.Field(context => context.Name).Type<StringType>();
        descriptor.Field(context => context.Total).Type<FloatType>();
        
    }
}