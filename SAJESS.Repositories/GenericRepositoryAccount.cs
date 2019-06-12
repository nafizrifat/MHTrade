using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SAJESS.Repositories
{
    public class GenericRepositoryAccount<T> : IGenericRepository<T> where T : class
    {
        private readonly Entities.Entities _db;
        private readonly DbSet<T> _aTable;

        public GenericRepositoryAccount(Entities.Entities db)
        {
            _db = db;
            _aTable = db.Set<T>();
        }
        public GenericRepositoryAccount()
        {
            _db = new Entities.Entities();
            _aTable = _db.Set<T>();
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public IEnumerable<T> SelectAll()
        {
            return _aTable.ToList();
        }

        public T SelectedById(object id)
        {
            return _aTable.Find(id);
        }

        public void Insert(T obj)
        {
            _aTable.Add(obj);
        }

        public void Update(T obj)
        {
            _aTable.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T find = _aTable.Find(id);
            _aTable.Remove(find);
        }

        public void Save()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
