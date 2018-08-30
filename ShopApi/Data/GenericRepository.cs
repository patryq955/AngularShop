using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.Helpers;

namespace ShopApi.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIDAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        TEntity GetByID(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetListAsync(
                   Expression<Func<TEntity, bool>> filter = null,
                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                   params Expression<Func<TEntity, object>>[] includes);

        Task<PagedList<TEntity>> GetPagedListAsync(
                    TParams tParams,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                    Expression<Func<TEntity, bool>> filter = null,
                    params Expression<Func<TEntity, object>>[] includes);
        Task InsertAsync(TEntity entity);
        void Delete(TEntity entity);

        void Delete(object id);


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

        public async virtual Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
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

        public async Task<PagedList<TEntity>> GetPagedListAsync(
            TParams tParams,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = orderBy(query).AsQueryable();
            
            return await PagedList<TEntity>.CreateAsync(query, tParams.PageNumber, tParams.PageSize);
        }

        public async Task<TEntity> GetByIDAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            query = query.Where(predicate);

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var tmp = await query.FirstOrDefaultAsync();
            return tmp;
        }

        public TEntity GetByID(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            query = query.Where(predicate);

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var tmp = query.First();
            return tmp;
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
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