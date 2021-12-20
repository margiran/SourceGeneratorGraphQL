using GraphQLProject.GraphQL;
using GraphQLProject.GraphQL.CategoryRecords;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLProject.SetupServices
{
    public class GraphQLInstaller : IServiceInstallers
    {
        public void InstallService(IConfiguration Configuration, IServiceCollection services, IWebHostEnvironment env)
        {
            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddType<CategoryType>()
                .AddType<AddCategoryInputType>()
                .AddType<AddCategoryPayloadType>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();
        }
    }
}