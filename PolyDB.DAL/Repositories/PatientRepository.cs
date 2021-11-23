using PolyDB.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyDB.DAL.Repositories
{
    public class PatientRepository : BaseRepository<PatientEntity, int>
    {
        public PatientRepository(IDBConnect Db): base(Db)
        {

        }
        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<PatientEntity> Get()
        {
            List<PatientEntity> retour = new List<PatientEntity>();
            if(_db.Connect())
            {
               List<Dictionary<string,Object>> datas = _db.getData("Select * from Patient");
                foreach (Dictionary<string, Object> item in datas)
                {
                    retour.Add(MapToDb(item));
                }
            }
            return retour;
        }

        public override PatientEntity GetOne(int id)
        {
            PatientEntity retour = null;
            if (_db.Connect())
            {
                //Remarque : Ne pas utiliser en PROD car risque d'injection SQL!!!!!
                List<Dictionary<string, Object>> datas = _db.getData($"Select * from Patient Where Id={id}");
                
                    retour = MapToDb(datas.FirstOrDefault());
                _db.Disconnect();
            }
            return retour;
        }

        public override bool Insert(PatientEntity toInsert)
        {
            bool retour = false;


            if (_db.Connect())
            {
                //Remarque : Ne pas utiliser en PROD car risque d'injection SQL!!!!!
                  retour = _db.Insert(@$"
INSERT INTO[dbo].[Patient]
           ([Nom]
           ,[Prenom]
           ,[DateNaissance])
     VALUES
           (@Nom
           ,@Prenom
           ,@DateNaissance)", MapToDico(toInsert));
                _db.Disconnect();
            }
            return retour;
        }

        public override bool Update(PatientEntity toUpdate)
        {
            throw new NotImplementedException();
        }

        private PatientEntity MapToDb(Dictionary<string,object> datas)
        {
            return new PatientEntity()
            {
                Id = (int)datas["Id"],
                Nom = datas["Nom"].ToString(),
                Prenom = datas["Prenom"].ToString()
            };
        }
    }
}
