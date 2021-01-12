using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Internal;

namespace GameStore.Bll.Tests.Fake
{
    public class BaseRepositoryFake<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly List<TEntity> _dbSet;

        public BaseRepositoryFake()
        {
            _dbSet = new List<TEntity>();
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
            var entityInDb = _dbSet.Single(x => x.Id == entity.Id);
            _dbSet.Remove(entityInDb);
            _dbSet.Add(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            var entityIdsToRemove = entities.Select(x => x.Id);
            var entitiesInDb = _dbSet.Where(x => entityIdsToRemove.Contains(x.Id));
            foreach (var entity in entitiesInDb)
            {
                _dbSet.Remove(entity);
            }

            _dbSet.AddRange(entities);
        }

        public void HardDelete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void HardDeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbSet.Remove(entity);
            }
        }

        public void SoftDelete(TEntity entity)
        {
            _dbSet.Single(x => x.Id == entity.Id).IsDeleted = true;
        }

        public void RecoverSoftDeleted(TEntity entity)
        {
            _dbSet.Single(x => x.Id == entity.Id).IsDeleted = false;
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Single(x => x.Id == id);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsQueryable().Any(predicate);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsQueryable().Where(predicate);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return _dbSet.AsQueryable().Where(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsQueryable().Single(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return _dbSet.AsQueryable().Single(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public IEnumerable<TEntity> GetAll<TSortField>(Expression<Func<TEntity, TSortField>> orderBy, bool ascending)
        {
            return _dbSet;
        }

        public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return _dbSet;
        }
    }
}