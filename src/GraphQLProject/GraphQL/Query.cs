using System.Linq;
using System.Threading.Tasks;
using GraphQLProject.Models;
using GraphQLProject.Services;
using HotChocolate;
using HotChocolate.Data;

namespace GraphQLProject.GraphQL
{
    [GraphQLDescription("Represents....")]
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Gets the queryable Category.")]
        public async Task< IQueryable<Category>> GetCategory([ScopedService] ICategoryService service)
        {
            return await service.BrowseAsync();
        }

    }
}
