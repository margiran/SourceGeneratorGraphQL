using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GraphQLProject.Repositories
{
    public class Repository<TDocument,TKey> : IRepository<TDocument,TKey> where TDocument :class where TKey :IEquatable<TKey>
    {

         private IMongoDatabase _database;

        public Repository(IMongoDatabase database)
        {
            _database = database;
        }

        // public async Task<TEntity> GetAsync(string name)
        //     => await Collection
        //         .AsQueryable()
        //         .FirstOrDefaultAsync(x => x.Name == name.ToLowerInvariant());

        public async Task<IQueryable<TDocument>> BrowseAsync()
            =>  Collection.AsQueryable();

   
            private IMongoCollection<TDocument> Collection =>
            _database.GetCollection<TDocument>($"{typeof(TDocument).Name}" );
        public async  Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument, bool>> predicate)
        {
            return await Collection.AsQueryable().Where(predicate).ToListAsync();
        }

        public async Task DeleteAsync(TKey id)
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", id);
            
            await Collection.FindOneAndDeleteAsync(filter);
        }

        public async Task<TDocument> FindByIdAsync(TKey id)
        {
             var filter = Builders<TDocument>.Filter.Eq("Id", id);
            
           return await Collection.FindOneAndDeleteAsync(filter);
        }

        public async Task AddAsync(TDocument document)
        {
            await Collection.InsertOneAsync(document);     
        }

    }
}
