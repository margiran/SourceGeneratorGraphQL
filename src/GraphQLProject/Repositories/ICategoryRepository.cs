using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLProject.Models;

namespace GraphQLProject.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetAsync(string name);
        Task<IQueryable<Category>> BrowseAsync();
        Task AddAsync(Category category);
        Task DeleteAsync(Guid id);

    }
}
