using System.Collections.Generic;

namespace PolyDB.DAL
{
    public interface IDBConnect
    {
        bool Connect();
        bool Disconnect();
        List<Dictionary<string, object>> getData(string Query);
        bool Insert(string Query, Dictionary<string,object> parametres);
    }
}