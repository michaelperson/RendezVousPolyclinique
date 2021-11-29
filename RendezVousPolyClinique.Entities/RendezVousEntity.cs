using DataTools.Interfaces;
using DataTools.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendezVousPolyClinique.Entities
{
    public class RendezVousEntity : IEntity<GuidPK, Guid>
    {
        private Guid _id;
        private DateTime _dateRendezVous;
        private string _medecin;
        private int idPatient;
        private PatientEntity _patient;

        public Guid Id
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

        public DateTime DateRendezVous
        {
            get
            {
                return _dateRendezVous;
            }

            set
            {
                _dateRendezVous = value;
            }
        }

        public string Medecin
        {
            get
            {
                return _medecin;
            }

            set
            {
                _medecin = value;
            }
        }

        public int IdPatient
        {
            get
            {
                return idPatient;
            }

            set
            {
                idPatient = value;
            }
        }

        public PatientEntity Patient
        {
            get
            {
                return _patient;
            }

            set
            {
                _patient = value;
            }
        }

        GuidPK IEntity<GuidPK, Guid>.Id
        {
            get
            {
                return new GuidPK() { Pk1 = this.Id };
            }
        }
    }
}
