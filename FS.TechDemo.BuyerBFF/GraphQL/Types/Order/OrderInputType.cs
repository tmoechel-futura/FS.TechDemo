using Shared;

namespace FS.TechDemo.BuyerBFF.GraphQL.Types.Order;

public class OrderInputType : InputObjectType<OrderCreateRequest>
{
    protected override void Configure(IInputObjectTypeDescriptor<OrderCreateRequest> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(context => context.Description).Type<StringType>();
        descriptor.Field(context => context.Name).Type<StringType>();
    }
}