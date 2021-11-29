using DataTools.Interfaces;
using DataTools.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendezVousPolyClinique.Entities
{
    public class PatientEntity:IEntity<IntPK,int>
    {
        private int _id;
        private string _nom;
        private string _prenom;
        private DateTime _dateNaissance;

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string Nom
        {
            get
            {
                return _nom;
            }

            set
            {
                _nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return _prenom;
            }

            set
            {
                _prenom = value;
            }
        }

        public DateTime DateNaissance
        {
            get
            {
                return _dateNaissance;
            }

            set
            {
                _dateNaissance = value;
            }
        }

        IntPK IEntity<IntPK, int>.Id
        {
            get
            {
                return new IntPK() { Pk1 = this.Id };
            }
        }
    }
}
