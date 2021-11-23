using ApiClient.Interfaces;
using RendezVousPloyclinique.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class PatientRequester : IRequester<PatientModel>
    {
        private readonly HttpClient _httpClient;
        public PatientRequester()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:11232/api/");
            _httpClient.Timeout = new TimeSpan(0, 1, 00);
            //Le type de contenu attendu
            _httpClient.DefaultRequestHeaders.Clear();

            _httpClient.DefaultRequestHeaders.Accept.Add
                (new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<PatientModel>> Get(string route)
        {
            HttpResponseMessage reponse = await _httpClient.GetAsync("Patient");
            reponse.EnsureSuccessStatusCode(); //Envoie une exception en cas de non success
            string jsonResult = await reponse.Content.ReadAsStringAsync();
            IEnumerable<PatientModel> Models =
                JsonSerializer.Deserialize<IEnumerable<PatientModel>>(jsonResult);
            return Models.ToList();
        }

        public  Task<bool> Post(string route, PatientModel element)
        {
            return SendToApi(route, element, HttpMethod.Post);
        }

     

        public Task<bool> Put(string route, PatientModel element)
        {
            return SendToApi(route, element, HttpMethod.Put);
        }

        public Task<bool> Delete(string route)
        {
            throw new NotImplementedException();
        }


        private async Task<bool> SendToApi(string route, PatientModel element, HttpMethod method)
        {
            //1. Seréaliser l'objet en json
            string contentJson = JsonSerializer.Serialize(element);
            //Je préare mon message de request
            HttpRequestMessage request = new HttpRequestMessage(method,route);
            request.Headers.Accept.Add
                (new MediaTypeWithQualityHeaderValue("application/json"));
            //Le contenu
            request.Content = new StringContent(contentJson);
            //le format d'envoi
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //JE contact l'API
            try
            {
                HttpResponseMessage response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
          

        }
    }
}
