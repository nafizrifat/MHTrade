using System;
using System.Collections.Generic;

namespace SAJESS.Repositories
{
  public  interface IGenericRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> SelectAll();
        T SelectedById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}
