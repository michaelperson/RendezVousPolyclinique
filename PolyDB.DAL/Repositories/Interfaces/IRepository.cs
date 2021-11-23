using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyDB.DAL.Repositories.Interfaces
{
    public interface IRepository<T,U>
        where T:class, new()
    {
        IEnumerable<T> Get();
        T GetOne(U id);

        bool Insert(T toInsert);
        bool Update(T toUpdate);
        bool Delete(U id);
    }
}
