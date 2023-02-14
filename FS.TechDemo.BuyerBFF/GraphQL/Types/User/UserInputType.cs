using System.Reflection.Metadata.Ecma335;

namespace FS.TechDemo.BuyerBFF.GraphQL.Types.User;

public class UserInputType: ObjectTypeBase<Keycloak.Net.Models.Users.User>
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Id { get; set; } = "";
}