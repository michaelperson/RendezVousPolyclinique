using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyDB.DAL.Entities
{
    public class PatientEntity
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
    }
}
