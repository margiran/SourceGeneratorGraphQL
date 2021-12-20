using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQLProject.Models;

namespace GraphQLProject.Services
{
    public interface ICategoryService
    {
        Task AddAsync(string name);
        Task<IQueryable<Category>> BrowseAsync();
    }
}
