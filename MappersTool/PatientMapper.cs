using PolyDB.DAL.Entities;
using RendezVousPloyclinique.Models;
using System;

namespace MappersTool
{
    public static class PatientMapper
    {

        public static PatientEntity MapToEntity(PatientModel patient)
        {
            return new PatientEntity()
            {
                Id = patient.Id,
                DateNaissance = patient.DateNaissance,
                Nom = patient.Nom,
                Prenom = patient.Prenom
            };
        }
        public static PatientModel  MapToModel(PatientEntity patient)
        {
            return new PatientModel()
            {
                Id = patient.Id,
                DateNaissance = patient.DateNaissance,
                Nom = patient.Nom,
                Prenom = patient.Prenom
            };
        }

    }
}
