using System;
using System.Data.Entity;
using MVCTask1EF;
using MVCTask1Model.RepositoryInterfaces;

namespace MVCTask1Model.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MVCTask1Entities _dbEntities;

        protected Repository(MVCTask1Entities context)
        {
            _dbEntities = context;
        }

        protected MVCTask1Entities DbEntities
        {
            get { return _dbEntities; }
        }

        public TEntity GetByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("key argument must not be null and empty");

            TEntity entity = null;

            entity = _dbEntities.Set<TEntity>().Find(key);

            if(entity == null)
                throw new InvalidOperationException($"{typeof (TEntity).Name} with ID={key} was not found in the DB");

            return entity;
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException($"entity argument with type {typeof (TEntity).Name} must not be null and empty");

            _dbEntities.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException($"entity argument with type {typeof(TEntity).Name} must not be null and empty");

            _dbEntities.Set<TEntity>().Attach(entity);
            _dbEntities.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException($"entity argument with type {typeof(TEntity).Name} must not be null and empty");

            if (_dbEntities.Entry(entity).State == EntityState.Detached)
            {
                _dbEntities.Set<TEntity>().Attach(entity);
            }
            _dbEntities.Set<TEntity>().Remove(entity);
        }

        public void Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("key argument must not be null and empty");

            var entityToDelete = _dbEntities.Set<TEntity>().Find(key);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
            else throw new InvalidOperationException($"{typeof (TEntity).Name} with ID={key} was not found in the DB");
        }
    }
}
