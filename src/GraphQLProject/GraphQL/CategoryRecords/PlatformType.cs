using System.Linq;
using GraphQLProject.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQLProject.GraphQL.CategoryRecords
{
    public class CategoryType : ObjectType<Category>
    {
        protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
        {
            descriptor.Description("Represents any software or service that has a command line interface.");

            descriptor
                .Field(p => p.Id)
                .Description("Represents the unique ID for the platform.");

            descriptor
                .Field(p => p.Name)
                .Description("Represents the name for the platform.");

        }
    }
}