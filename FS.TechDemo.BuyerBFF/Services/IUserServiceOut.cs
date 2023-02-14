using FS.TechDemo.BuyerBFF.GraphQL.Types.User;
using Google.Protobuf.WellKnownTypes;
using Keycloak.Net.Models.Users;
using Shared;

namespace FS.TechDemo.BuyerBFF.Services;

public interface IUserServiceOut
{
    Task<IEnumerable<User>> GetUserListAsync(CancellationToken cancellationToken);
    Task<Int32Value> CreateUser(UserInputType inputType, CancellationToken cancellationToken);
}
