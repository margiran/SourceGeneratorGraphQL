using HotChocolate.Types;

namespace GraphQLProject.GraphQL.CategoryRecords
{
    public class AddCategoryInputType : InputObjectType<AddCategoryInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddCategoryInput> descriptor)
        {
            descriptor.Description("Represents the input to add for a platform.");

            descriptor
                .Field(p => p.Name)
                .Description("Represents the name for the platform.");

            base.Configure(descriptor);
        }
    }
}
