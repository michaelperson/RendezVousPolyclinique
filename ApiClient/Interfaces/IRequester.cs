using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Interfaces
{
    public interface IRequester<T>
         where T : class
    {
        //route : /Patient/0
        Task<List<T>> Get(string route);
        Task<bool> Post(string route, T element);
        Task<bool> Put(string route, T element);

        Task<bool> Patch(string route, JsonPatchDocument<T> element);
        Task<bool> Delete(string route);



    }
}
