using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetByID(object id);
        IEnumerable<TEntity> Get(
                   Expression<Func<TEntity, bool>> filter = null,
                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                   string includeProperties = "");
        void Insert(TEntity entity);
        void Delete(TEntity entity);

        void Update(TEntity entityToUpdate);
    }

    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DataContext _db;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DataContext db)
        {
            _db = db;
            this.dbSet = db.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_db.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _db.Entry(entityToUpdate).State = EntityState.Modified;
        }


    }
}