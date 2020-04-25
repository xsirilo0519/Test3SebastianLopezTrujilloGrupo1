using System;
using System.Collections.Generic;
using System.Text;
using test3.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

        public async Task<bool> PutAsync(string urlBase, string servicePrefix, string controller,SolutionRequest solution , string tokenType, string accessToken)
        {
            try
            {
                string request = JsonConvert.SerializeObject(solution);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PutAsync(url, content);
                string answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
