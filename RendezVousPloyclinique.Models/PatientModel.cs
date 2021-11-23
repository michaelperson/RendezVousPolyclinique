using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RendezVousPloyclinique.Models
{
    public class PatientModel
    {
    
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateNaissance { get; set; }
    }
}
