using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using RendezVousPloyclinique.Models;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace RendezVousPolyclinique.Infra.Formatters
{
    public class HL7Formatter : TextOutputFormatter
    {
        public HL7Formatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/HL7"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(IEnumerable<PatientModel>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        public async override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            // 1 Récupérer l'objet de réponse
            IEnumerable<PatientModel> Patients = 
                (IEnumerable<PatientModel>)context.Object;
            // 2. Je fais ce que je veux
            string HL7String = ConvertToHL7(Patients);
            //3. J'écris dans la réponse 
            //3.1 Récupération de la réponse
            HttpResponse reponse = context.HttpContext.Response;
            //3.2 Ecriture
            await reponse.WriteAsync(HL7String);
            
        }

        private string ConvertToHL7(IEnumerable<PatientModel> patients)
        {
            StringBuilder retour = new StringBuilder();
            foreach (PatientModel item in patients)
            {
                retour.AppendLine($"{item.Id} | {item.Nom} | {item.Prenom}");
            }
            return retour.ToString();
        }
    }
}
