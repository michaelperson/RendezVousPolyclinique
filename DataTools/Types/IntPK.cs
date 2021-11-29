using DataTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTools.Types
{
    public class IntPK : IPK<int>
    {
        
        public int Pk1
        {
            get;set;
        }
    }
}
