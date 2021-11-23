using PolyDB.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PolyDB.DAL.Repositories
{
    public abstract class BaseRepository<T, U> : IRepository<T, U>
        where T: class, new()
    {
        protected readonly IDBConnect _db;
        protected BaseRepository(IDBConnect dB)
        {
            _db = dB;
        }

        public abstract IEnumerable<T> Get();

        public abstract T GetOne(U id);

        public abstract bool Insert(T toInsert);

        public abstract bool Update(T toUpdate);

        public abstract bool Delete(U id);

        protected Dictionary<string, object> MapToDico(T item)
        {
            Dictionary<string, object> dico = new Dictionary<string, object>();
            foreach (PropertyInfo p in item.GetType().GetProperties())
            {
                dico.Add(p.Name, p.GetValue(item));
            }
            return dico;
        }
    }
}
