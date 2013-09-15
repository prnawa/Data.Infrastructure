using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Data.Infrastructure.Core;
using Domain.Core.DomainEntities;

namespace Data.Infrastructure.EF
{
    public abstract class GenericRepositoryEF<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : EntityBase<TKey> 
    {
        private DbContext _context;
        private IDbSet<TEntity> _dbSet; 

        private DbContext Context
        {
            get { return _context ?? (_context = GetCurrentContext()); }
        }

        private DbContext GetCurrentContext()
        {
            return ((UnitOfWorkEF) UnitOfWork.Current).Context;
        }

        private IDbSet<TEntity> DbSet
        {
            get { return _dbSet ?? (_dbSet = Context.Set<TEntity>()); }
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public TEntity GetById(TKey key)
        {
            return DbSet.Find(key);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Single(predicate);
        }

        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.First(predicate);
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }       
    }
}
