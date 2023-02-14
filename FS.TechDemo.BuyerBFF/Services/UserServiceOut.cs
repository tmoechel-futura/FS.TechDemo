using System.Collections;
using FS.TechDemo.BuyerBFF.Configuration;
using FS.TechDemo.BuyerBFF.GraphQL.Types.User;
using FS.TechDemo.BuyerBFF.IdentityProvider;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using HotChocolate.AspNetCore.Authorization;
using Keycloak.Net.Models.Users;
using Microsoft.Extensions.Options;
using Shared;

namespace FS.TechDemo.BuyerBFF.Services;

internal class UserServiceOut : IUserServiceOut
{
    private readonly IConfiguration _configuration;
    private readonly IIdentityProviderAdapter<User> _identityProviderAdapter;
    private readonly IOptions<GrpcOptions> _grpcOptions;
    private readonly ILogger<UserServiceOut> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UserServiceOut(IConfiguration configuration, 
        IIdentityProviderAdapter<Keycloak.Net.Models.Users.User> identityProviderAdapter,
        IOptions<GrpcOptions> grpcOptions, ILogger<UserServiceOut> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _configuration = configuration;
        _identityProviderAdapter = identityProviderAdapter;
        _grpcOptions = grpcOptions;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }


    public async Task<IEnumerable<User>> GetUserListAsync(CancellationToken cancellationToken)
    {
        return await _identityProviderAdapter.GetUserListAsync();
    }

    public Task<Int32Value> CreateUser(UserInputType inputType, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
