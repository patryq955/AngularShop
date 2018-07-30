using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApi.Data
{
    public interface IGenericUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;
    }
    public class GenericUnitOfWork : IDisposable, IGenericUnitOfWork
    {
        private DataContext _db;
        public GenericUnitOfWork(DataContext db)
        {
            _db = db;
        }
        // Słownik będzie używany do sprawdzania instancji repozytoriów
        private Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories.Keys.Contains(typeof(T)) == true)
            {
                return _repositories[typeof(T)] as IRepository<T>;
            }

            var repo = new GenericRepository<T>(_db);
            _repositories.Add(typeof(T), repo);
            return repo;
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _db.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
