using FS.TechDemo.BuyerBFF.GraphQL;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// graphql specific
builder.Services
    .AddGraphQLServer()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddQueryType<BuyerQuery>()
    .AddMutationType<BuyerMutation>();

app.Run();