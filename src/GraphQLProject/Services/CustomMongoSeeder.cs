using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLProject.Models;
using GraphQLProject.Mongo;
using GraphQLProject.Repositories;
using MongoDB.Driver;

namespace GraphQLProject.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository _categoryRepository;
        public CustomMongoSeeder(IMongoDatabase database, ICategoryRepository categoryRepository) : base(database)
        {
            _categoryRepository = categoryRepository;
        }

        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string>
            {
                "work",
                "sport"
            };
            await Task.WhenAll(categories.Select(x =>
                _categoryRepository.AddAsync(new Category(x))));
        }
    }
}
