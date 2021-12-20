using HotChocolate.Types;

namespace GraphQLProject.GraphQL.CategoryRecords
{
    public class AddCategoryPayloadType : ObjectType<AddCategoryPayload>
    {
        protected override void Configure(IObjectTypeDescriptor<AddCategoryPayload> descriptor)
        {
            descriptor.Description("Represents the payload to return for an added platform.");

            descriptor
                .Field(p => p.category)
                .Description("Represents the added platform.");

            base.Configure(descriptor);
        }
    }
}
