using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGISYSS.Entities;

namespace DIGISYSS.Repositories
{
    public class GenericRepositoryAccounting<T> : IGenericRepository<T> where T : class
    {
        private readonly MarketManagementAccountingEntities _db;
        private readonly DbSet<T> _aTable;

        public GenericRepositoryAccounting(MarketManagementAccountingEntities db)
        {
            _db = db;
            _aTable = db.Set<T>();
        }
        public GenericRepositoryAccounting()
        {
            _db = new MarketManagementAccountingEntities();
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
