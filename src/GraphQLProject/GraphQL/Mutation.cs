using System;
using System.Threading;
using System.Threading.Tasks;
using GraphQLProject.Models;
using GraphQLProject.Repositories;
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
            [Service] IRepository<Category,Guid> service,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken
            ) 
            {
                var category = new Category(input.Name);

                await service.AddAsync(category);

                //await eventSender.SendAsync(nameof(Subscription.OnCategoryAdded), category, cancellationToken);

                return new AddCategoryPayload(category);
            }
    }
}