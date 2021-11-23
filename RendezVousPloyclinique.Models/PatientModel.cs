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
        [JsonPropertyName("nom")]
        [MinLength(2)]
        public string Nom { get; set; }
        [Required]

        [JsonPropertyName("prenom")]
        public string Prenom { get; set; }
        [Required]
        [DataType(DataType.DateTime)]

        [JsonPropertyName("dateNaissance")]
        public DateTime DateNaissance { get; set; }
    }
}
