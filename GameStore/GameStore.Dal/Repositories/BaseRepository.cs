using GameStore.Dal.Contexts;
using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.Dal.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly IQueryable<TEntity> _queryableDbSet;

        public BaseRepository(GameStoreContext context)
        {
            _dbSet = context.Set<TEntity>();
            _queryableDbSet = _dbSet;
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void HardDelete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void HardDeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void SoftDelete(TEntity entity)
        {
            _dbSet.Find(entity.Id).IsDeleted = true;
        }

        public void RecoverSoftDeleted(TEntity entity)
        {
            _dbSet.Find(entity.Id).IsDeleted = false;
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return include(_queryableDbSet).AsNoTracking().Where(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Single(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return include(_queryableDbSet).AsNoTracking().Single(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IEnumerable<TEntity> GetAll<TSortField>(Expression<Func<TEntity, TSortField>> orderBy, bool ascending)
        {
            return ascending ? _dbSet.OrderBy(orderBy) : _dbSet.OrderByDescending(orderBy);
        }

        public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return include(_queryableDbSet).AsNoTracking();
        }
    }
}