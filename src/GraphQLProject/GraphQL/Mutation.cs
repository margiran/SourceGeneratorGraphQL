using System.Threading;
using System.Threading.Tasks;
using GraphQLProject.GraphQL.CategoryRecords;
using GraphQLProject.Models;
using GraphQLProject.Services;
using HotChocolate;
using HotChocolate.Subscriptions;

namespace GraphQLProject.GraphQL
{
    [GraphQLDescription("mutations available.")]
    public class Mutation
    {
        [GraphQLDescription("Adds a Category.")]
        public async Task<AddCategoryPayload> AddCategoryAsync(
            AddCategoryInput input,
            [ScopedService] ICategoryService service,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken
            ) 
            {
                var category = new Category{
                    Name=input.Name
                };

                await service.AddAsync(input.Name);

                await eventSender.SendAsync(nameof(Subscription.OnCategoryAdded), category, cancellationToken);

                return new AddCategoryPayload(category);
            }
    }
}