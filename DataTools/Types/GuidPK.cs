using DataTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTools.Types
{
    public class GuidPK : IPK<Guid>
    {
        public Guid Pk1
        {
            get;set;
        }
    }
}
