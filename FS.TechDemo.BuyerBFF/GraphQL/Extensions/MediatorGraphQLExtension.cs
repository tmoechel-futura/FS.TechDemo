using HotChocolate.Resolvers;
using MediatR;

namespace FS.TechDemo.BuyerBFF.GraphQL.Extensions;

public static class MediatorGraphQLExtension
{
    public static Func<IResolverContext, Task<object?>> GetResolverFunc<TResolvableRequest>(
        this IMediator mediator, ILoggerFactory loggerFactory)
        where TResolvableRequest : ResolvableRequest, new()
    {
        async Task<object?> resolverFunc(IResolverContext resolveFieldContext)
        {
            var request = new TResolvableRequest();
            request.Configure(mediator, resolveFieldContext);
            var response = await mediator.Send(request, resolveFieldContext.RequestAborted);
            return response;
        }

        var logger = loggerFactory.CreateLogger<TResolvableRequest>();
        logger.LogInformation("Resolver function loaded.");
        return resolverFunc;
    }
}
