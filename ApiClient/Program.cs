using ApiClient.Interfaces;
using RendezVousPloyclinique.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    class Program
    {
        static async Task  Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IRequester<PatientModel> requester = new PatientRequester();

            //get
            foreach( PatientModel item in await requester.Get("Patient"))
            {
                Console.WriteLine($"Nom : {item.Nom}");
            }

            //POST
            PatientModel pm = new PatientModel() { Id = 1, Nom = "PtitGoutte", Prenom = "Emma", 
                DateNaissance= new DateTime(1926,3,15) };
            if(await requester.Post("Patient", pm))
            {
                Console.WriteLine("Patient inseré");
            }
            else
            {
                Console.WriteLine("Patient pas inseré");
            }
            //get
            foreach (PatientModel item in await requester.Get("Patient"))
            {
                Console.WriteLine($"Nom : {item.Nom}");
            }
        }
    }
}
