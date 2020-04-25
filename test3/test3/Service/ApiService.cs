using System;
using System.Collections.Generic;
using System.Text;
using test3.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace test3.Service
{
    public class ApiService:IApiService
    {
        public async Task<Response> GetProblemAsync(string urlBase, string servicePrefix, string controller, UserRequest request)
        {
            try
            {
                string requestString = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(requestString, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<Response>(result);
                Response hola = JsonConvert.DeserializeObject<Response>(result);
                return hola;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
