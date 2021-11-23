using PolyDB.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyDB.DAL.Repositories
{
    public class RendezVousRepository : BaseRepository<RendezVousEntity, Guid>
    {
        public RendezVousRepository(IDBConnect Db) : base(Db)
        {

        }

        public override bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<RendezVousEntity> Get()
        {
            throw new NotImplementedException();
        }

        public override RendezVousEntity GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public override bool Insert(RendezVousEntity toInsert)
        {
            throw new NotImplementedException();
        }

        public override bool Update(RendezVousEntity toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
