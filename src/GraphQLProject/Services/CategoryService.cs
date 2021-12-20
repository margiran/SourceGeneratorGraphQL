using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQLProject.Models;
using GraphQLProject.Repositories;

namespace GraphQLProject.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public async Task AddAsync( string name)
        {
            // var category = await _categoryRepository.GetAsync(name);
            await _categoryRepository.AddAsync(new Category { Name= name});
        }

        public async Task<IQueryable<Category>> BrowseAsync()
        {
            return await _categoryRepository.BrowseAsync();
         }
    }
}
