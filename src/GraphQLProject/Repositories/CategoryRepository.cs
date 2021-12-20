using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLProject.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GraphQLProject.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private IMongoDatabase _database;

        public CategoryRepository(IMongoDatabase database)
        {
            _database = database;
        }



        public async Task<Category> GetAsync(string name)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Name == name.ToLowerInvariant());

        public async Task<IQueryable<Category>> BrowseAsync()
            =>  Collection.AsQueryable();

        public async Task AddAsync(Category category)
            => await Collection.InsertOneAsync(category);

        public async Task DeleteAsync(Guid id)
            => await Collection.DeleteOneAsync(x => x.Id == id);

        private IMongoCollection<Category> Collection =>
            _database.GetCollection<Category>("Categories");
    }
}
