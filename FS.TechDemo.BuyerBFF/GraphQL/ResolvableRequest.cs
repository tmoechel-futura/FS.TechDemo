using HotChocolate.Resolvers;
using MediatR;

namespace FS.TechDemo.BuyerBFF.GraphQL;

public abstract class ResolvableRequest : IRequest<object>
{
    protected IResolverContext? _resolveFieldContext;
    public IResolverContext GetResolveFieldContext()
        => _resolveFieldContext ?? throw new ArgumentNullException(nameof(_resolveFieldContext));

    private IMediator? _mediator;
    public IMediator Mediator => _mediator ?? throw new ArgumentNullException($"{nameof(Mediator)} is not registered");

    public void Configure(IMediator mediator, IResolverContext resolveFieldContext)
    {
        _resolveFieldContext = resolveFieldContext;
        _mediator = mediator;
    }
}
