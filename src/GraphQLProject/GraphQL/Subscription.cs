using GraphQLProject.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQLProject.GraphQL
{
    [GraphQLDescription("Represents....")]
    public class Subscription
    {
        [Subscribe]
        [Topic]
        [GraphQLDescription("The subscription for added Category")]
        public Category OnCategoryAdded([EventMessage] Category category)
        {
            return category;
        }
    }
}