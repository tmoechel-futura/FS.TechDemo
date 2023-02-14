using FS.TechDemo.BuyerBFF.Configuration;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Shared;

namespace FS.TechDemo.BuyerBFF.Services;

internal class UserServiceOut : IUserServiceOut
{
    private readonly IConfiguration _configuration;
    private readonly IOptions<GrpcOptions> _grpcOptions;
    private readonly ILogger<OrderServiceOut> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UserServiceOut(IConfiguration configuration, IOptions<GrpcOptions> grpcOptions, ILogger<OrderServiceOut> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _configuration = configuration;
        _grpcOptions = grpcOptions;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }


    public Task<List<OrderResponse>> GetUserListAsync(CancellationToken cancellationToken)
    {
        
    }

    public Task<Int32Value> CreateUser(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
