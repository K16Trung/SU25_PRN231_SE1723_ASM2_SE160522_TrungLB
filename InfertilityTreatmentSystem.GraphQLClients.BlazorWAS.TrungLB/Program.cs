using InfertilityTreatmentSystem.GraphQLClients.BlazorWAS.TrungLB;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using TreatmentReminder.GraphQLClients.BlazorWAS.TrungLB.GraphQLClients;
using GraphQL.Client.Abstractions;
using InfertilityTreatmentSystem.GraphQLClients.BlazorWAS.TrungLB.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register GraphQL Client
builder.Services.AddScoped<IGraphQLClient>(sp =>
{
    // Point to the GraphQL API service endpoint
    return new GraphQLHttpClient("https://localhost:7139/graphql", new NewtonsoftJsonSerializer());
});

builder.Services.AddScoped<GraphQLConsumer>();

// Register Authentication Service
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();
