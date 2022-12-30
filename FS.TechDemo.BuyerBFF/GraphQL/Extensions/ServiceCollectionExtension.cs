using HotChocolate.Execution.Configuration;

namespace FS.TechDemo.BuyerBFF.GraphQL.Extensions;

public static class ServiceCollectionExtension
{
   
    public static IRequestExecutorBuilder AddGraphQLOptions<TEntryPoint>(this IServiceCollection serviceCollection)
        where TEntryPoint : class
    {
        var requestExecutorBuilder = serviceCollection.AddGraphQLServer();
                                                      
   
        //serviceCollection.AddFairyBreadValidators<TEntryPoint>(requestExecutorBuilder);

        serviceCollection.AddGraphQLSchemas<TEntryPoint>(requestExecutorBuilder);
            
         // .AddErrorFilter<ErrorHandlingFilter<TEntryPoint>>();

        //serviceCollection.AddHealthChecks();

        return requestExecutorBuilder;
    }

    private static IServiceCollection AddGraphQLSchemas<TEntryPoint>(this IServiceCollection serviceCollection, IRequestExecutorBuilder requestExecutorBuilder)
    {
        var classesList = typeof(TEntryPoint).Assembly.GetTypes();
        // Only one query or mutation type is allowed. To register multiple use type extensions: https://chillicream.com/docs/hotchocolate/defining-a-schema/extending-types
        var mutationType = classesList.SingleOrDefault(type=> type.IsAssignableTo(typeof(ObjectType)) && type.Name.ToLowerInvariant().EndsWith("mutation"));
        var queryType = classesList.SingleOrDefault(type=> type.IsAssignableTo(typeof(ObjectType)) && type.Name.ToLowerInvariant().EndsWith("query"));
        if (mutationType != null) requestExecutorBuilder.AddMutationType(mutationType);
        if (queryType != null) requestExecutorBuilder.AddQueryType(queryType);

        return serviceCollection;
    }
}
