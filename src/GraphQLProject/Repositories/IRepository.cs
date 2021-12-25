using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GraphQLProject.Repositories
{
    public    interface IRepository<TDocument,TKey> where TDocument :class where TKey :IEquatable<TKey>
    {
        Task<IQueryable<TDocument>> BrowseAsync();

        Task<TDocument> FindByIdAsync(TKey id);
        // Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        // Task UpdateAsync(TEntity entity);
        // Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task AddAsync(TDocument document);
        // Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TKey id);
        // Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        

    }
}
