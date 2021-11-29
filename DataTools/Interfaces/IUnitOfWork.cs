using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTools.Interfaces
{
    /// <summary>
    /// Interface qui conditionne l'implementation de notre UnitOfWork
    /// Chaque Repository sera ajouter ici pour qu'il puisse y avoir accès et ainsi l'implementer
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {




        bool Commit();
    }
}
