namespace FS.TechDemo.BuyerBFF.GraphQL.Types.User;

public class UserType: ObjectTypeBase<Keycloak.Net.Models.Users.User>
{
    protected override void Configure(IObjectTypeDescriptor<Keycloak.Net.Models.Users.User> descriptor)
    {
        base.Configure(descriptor);
        
        descriptor.Field(context => context.Id).Description("Id of the Order");
        descriptor.Field(context => context.Email).Description("Email of the User");
        descriptor.Field(context => context.FirstName).Description("First Name");
        descriptor.Field(context => context.LastName).Description("Last Name");
    }
}