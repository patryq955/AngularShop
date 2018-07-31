using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByID(object id);
        Task<IEnumerable<TEntity>> Get(
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

        public async virtual Task<IEnumerable<TEntity>> Get(
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
                var ordered = await orderBy(query).ToListAsync();
                return ordered;
            }
            else
            {
                var result = await query.ToListAsync();
                return result;
            }
        }
        public async Task<TEntity> GetByID(object id)
        {
            var tmp =  await dbSet.FindAsync(id);
            return tmp;
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