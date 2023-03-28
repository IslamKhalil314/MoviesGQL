using MoviesGQL.Data;
using Microsoft.EntityFrameworkCore;
using MoviesGQL.GQL;
using GraphQL.Server.Ui.Voyager;
using MoviesGQL.GQL.MovieTypes;
using MoviesGQL.GQL.ActorTypes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<AppDbContext>(opt =>
{

    opt/*.UseLazyLoadingProxies()*/.UseSqlServer(builder.Configuration.GetConnectionString("MovieDb"));
});
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutaion>()
    .AddSubscriptionType<Subscription>()
    .AddType<MovieType>()
    .AddType<ActorType>()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions();


var app = builder.Build();




app.UseRouting();
app.MapGet("/", () => "Hello World!");
app.MapGraphQL();
app.MapGraphQLVoyager("/gql", new VoyagerOptions() { GraphQLEndPoint = "/graphql" });



app.Run();
