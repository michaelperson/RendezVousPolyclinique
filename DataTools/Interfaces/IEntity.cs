using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTools.Interfaces
{
    public interface IEntity<TKey, TType>
        where TKey : IPK<TType>
    {
        TKey Id { get; }
    }
}
