using System;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Infrastructure.Core
{
    public interface IGenericRepository<TEntity, in TKey>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(TKey key);
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        TEntity First(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Delete(TEntity entity);
    }
}
